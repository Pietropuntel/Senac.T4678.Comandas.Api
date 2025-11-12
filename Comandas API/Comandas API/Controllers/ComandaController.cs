using System.Xml.Schema;
using Comandas_API.DTOS;
using Comandas_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {   
           public ComandasDbContext _context { get; set; }
        public ComandaController(ComandasDbContext context)
        {
            _context = context;
        }

        // GET: api/<ComandaController>
        [HttpGet]
        public IResult Get()
        {
            var comandas = _context.comandas.ToList();
            return Results.Ok(comandas);

        }

        // GET api/<ComandaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var comanda = _context.comandas.FirstOrDefault(c => c.Id == id);
            if (comanda is null)
            {
                return Results.NotFound("Comanda não encontrada");
                
            }
            return Results.Ok(comanda);
        }

        // POST api/<ComandaController>
        [HttpPost]
        public IResult Post([FromBody] ComandaCreateRequest comandaCreate)
        {
            if (comandaCreate.NomeCliente.Length < 3)
            {
                Results.BadRequest("O nome do cliente deve ter no mínimo 3 caracteres");
            }
            if (comandaCreate.NumeroMesa <= 0)
            {
                Results.BadRequest("O número da mesa deve ser maior que zero");
            }
            if (comandaCreate.CardapioItemIds.Length == 0)
            {
                Results.BadRequest("A comanda deve ter pelo menos um item");
            }
            var novaComanda = new Comanda
            {
                
                NomeCliente = comandaCreate.NomeCliente,
                NumeroMesa = comandaCreate.NumeroMesa,

            };
            var itensComanda = new List<ComandaItem>();
            foreach (int cardapioItemId in comandaCreate.CardapioItemIds)
            {
                var novoItemComanda = new ComandaItem
                {

                    CardapioItemId = cardapioItemId,
                    Comanda = novaComanda,
                };
                itensComanda.Add(novoItemComanda);

                // criar o pedido de cozinha eo item de acordo com o cadastro do cardapio possuipreparar
                var cardapioItem = _context.CardapioItens
                    .FirstOrDefault(ci => ci.Id == cardapioItemId);

                if (cardapioItem!.PossuiPreparo)
                {
                    var pedido = new PedidoCozinha
                    {
                        Comanda = novaComanda,
                    };
                    var pedidoItem = new PedidoCozinhaItem
                    {
                        ComandaItem = novoItemComanda,
                        PedidoCozinha = pedido,
                    };
                    _context.PedidoCozinhas.Add(pedido);
                    _context.PedidoCozinhasItens.Add(pedidoItem);
                }
            }
            novaComanda.Itens = itensComanda;
            _context.comandas.Add(novaComanda);
            _context.SaveChanges();

            var coco = new ComandaCreateResponse
            {
                Id = novaComanda.Id,
                NomeCliente = novaComanda.NomeCliente,
                NumeroMesa = novaComanda.NumeroMesa,
                Itens = novaComanda.Itens.Select(i => new ComandaItemResponse
                {
                    Id = i.Id,
                    Titulo = _context.CardapioItens.First(ci => ci.Id == i.CardapioItemId).Titulo
                }).ToList()
            };
            return Results.Created($"/api/comanda/{novaComanda.Id}", coco);

           
        }

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] ComandaUpdateRequest ComandaUpdate)
        {
            var comanda = _context.comandas.FirstOrDefault(c => c.Id == id);
            if (comanda is null)
                return Results.NotFound($"Comanda do id {id} Nao encontrado");
            if (ComandaUpdate.NomeCliente.Length < 3)
                return Results.NotFound("O nome do cliente deve ter no mínimo 3 caracteres");
            if (ComandaUpdate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero");

            comanda.NomeCliente = ComandaUpdate.NomeCliente;
            comanda.NumeroMesa = ComandaUpdate.NumeroMesa;


            //itens

            foreach (var item in ComandaUpdate.Itens)
            {

                //se id for informado e remover for verdadeiro 
                if (item.Id > 0 && item.Remove == true)
                {
                   RemoverItemComanda(item.Id);
                }

                //se crdapioItemId foi informado 
                if (item.CardapioItemId > 0 )
                {
                     InserirItemComanda(comanda, item.CardapioItemId);
                }
            }

            return Results.NoContent();
        }

        private void InserirItemComanda(Comanda comanda, int cardapioItemId)
        {
            _context.comandaItems.Add(new ComandaItem
            {
                CardapioItemId = cardapioItemId,
                Comanda = comanda,
                
            });
        }

        private void RemoverItemComanda(int id)
        {
          var ComandaItem = _context.comandaItems.FirstOrDefault
                (ci  => ci.Id == id);
            if (ComandaItem is not null )
            {
                _context.comandaItems.Remove(ComandaItem);
                
            }
        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var comanda = _context.comandas
                .FirstOrDefault(c => c.Id == id);
            if (comanda is null)
                return Results.NotFound("comanda não encontrada");
            _context.comandas.Remove(comanda);
            var removido = _context.SaveChanges();
            if (removido > 0)
                return Results.NotFound();
            return Results.StatusCode(500);

        }
    }
}
