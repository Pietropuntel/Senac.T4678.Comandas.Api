using Comandas_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoCozinhaController : ControllerBase
       
    {
        public ComandasDbContext _context { get; set; }

        public PedidoCozinhaController(ComandasDbContext context)
        {
            _context = context;
        }

        // GET: api/<PedidoCozinhaController>
        [HttpGet]
        public IResult Get()
        {
            var PedidoCozinha = _context.PedidoCozinhas.ToList();
            return Results.Ok(PedidoCozinha);
        }

        // GET api/<PedidoCozinhaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var pedido = _context.PedidoCozinhas.FirstOrDefault(p => p.Id == id);
            if (pedido is null)
            {
                return Results.NotFound("Pedido não encontrado");
                
            }
            return Results.Ok(pedido);
        }

        // POST api/<PedidoCozinhaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            //if (cardapio.Titulo.Length < 3)

            //    return Results.BadRequest("O titulo deve ter no mínimo 3 caracteres");
            //if (cardapio.Descricao.Length < 3)
            //    return Results.BadRequest("A descricao deve ter no mínimo 3 caracteres");
            //if (cardapio.Preco <= 0)
            //    return Results.BadRequest("O preço deve ser maior que zero");
            //var cardapioItem = new CardapioItem
            //{
            //    Id = PedidoCozinhaId.Count + 1,
            //    Titulo = PedidoCozinhaId.Titulo,
            //    Descricao = PedidoCozinhaId.Descricao,
            //    Preco = PedidoCozinhaId.Preco,
            //    PossuiPreparo = cardapio.PossuiPreparo
            //};
            ////adicionar o cardapio na lista
            //cardapios.Add(cardapioItem);
            //return Results.Created($"/api/cardapio/{cardapioItem.Id}", cardapioItem);
        }




        /// <summary>
        /// Atualiza um item do cardápio
        /// </summary>
        /// <param name="id"></param>
        /// <param name="PedidoCozinha"></param>
        // PUT api/<PedidiCozinhaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] string value)
        {
            var PedidoCozinha = _context.PedidoCozinhas.FirstOrDefault(c => c.Id == id);
            if (PedidoCozinha is null)
                return Results.NotFound($"Pedido Cozinha do id {id} Nao encontrado");
            
            return Results.NoContent();
        }

        // DELETE api/<PedidoCozinhaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var pedidosCozinha = _context.PedidoCozinhas
                .FirstOrDefault(c => c.Id == id);
            if (pedidosCozinha is null)
                return Results.NotFound("pedido não encontrado");
            _context.PedidoCozinhas.Remove(pedidosCozinha);
            var removido = _context.SaveChanges();
            if (removido > 0 )
            {
                return Results.NotFound();
            }
            return Results.StatusCode(500);
        }
    }
}
