using Comandas_API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoCozinhaController : ControllerBase
    {   
        List<PedidoCozinha> pedidos = new List<PedidoCozinha>()
        {
            new PedidoCozinha
            {
                Id = 1,
                ComandaId = 1,


            },
            new PedidoCozinha
            {
                Id = 2,
               ComandaId = 2,
            }
        };






        // GET: api/<PedidoCozinhaController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(pedidos);
        }

        // GET api/<PedidoCozinhaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var pedido = pedidos.FirstOrDefault(p => p.Id == id);
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
        public void Put(int id, [FromBody] string value)
        {
            var PedidoCozinha = pedidos.FirstOrDefault(c => c.Id == id);
            if (PedidoCozinha is null)
                return Results.NotFound($"Pedido Cozinha do id {id} Nao encontrado");
            PedidoCozinha.PedidoCozinhaId = PedidoCozinha.pedidos;
            PedidoCozinha.ComandaItemId = PedidoCozinha.ComandaId;
            return Results.NoContent();
        }

        // DELETE api/<PedidoCozinhaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
