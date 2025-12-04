using Comandas_API.DTOS;
using Comandas_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]// Define que essa classe é um controlador de API
    public class CardapioItemController : ControllerBase// Define que essa classe herda de ControllerBase
    {
        private ComandasDbContext _context;
        public CardapioItemController(ComandasDbContext context)
        {
            _context = context;
        }


    
        //Metodo GET que retorna uma lista de cardapio
        // GET: api/<CardapioItemController>
        [HttpGet]// Define que esse método responde a requisições GET
        public IEnumerable<CardapioItem> Get()
        {
        var cardapios = _context.CardapioItens.Include(ci => ci.CategoriaCardapio).ToList();
        return cardapios;
    }
       
        // GET api/<CardapioItemController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var cardapio = _context.CardapioItens.Include(ci => ci.CategoriaCardapio) .FirstOrDefault(c => c.Id == id);
            if(cardapio is null)
            {
                return Results.NotFound("Cardapio não encontrado!");
            }
            return Results.Ok(cardapio);
        }

        // POST api/<CardapioItemController>
        [HttpPost]
        public IResult Post([FromBody] CardapioItemCreateRequest cardapio)
        {
            if(cardapio.Titulo.Length < 3)
           
                return Results.BadRequest("O titulo deve ter no mínimo 3 caracteres");
            if(cardapio.Descricao.Length < 3 )
                return Results.BadRequest("A descricao deve ter no mínimo 3 caracteres");
            if (cardapio.Preco <= 0)
                return Results.BadRequest("O preço deve ser maior que zero");

            // Validação da categoria se for preenchida
            if (cardapio.CategoriaCardapioId.HasValue)
            {
                var categoria = _context.CategoriaCardapio
                    .FirstOrDefault(c => c.Id == cardapio.CategoriaCardapioId);
                if (categoria is null)
                    return Results.BadRequest("Categoria de cardápio inválida.");
            }

            var cardapioItem = new CardapioItem
            {
                
                Titulo = cardapio.Titulo,
                Descricao = cardapio.Descricao,
                Preco = cardapio.Preco,
                PossuiPreparo = cardapio.PossuiPreparo,
                CategoriaCardapioId = cardapio.CategoriaCardapioId
            };
            //adicionar o cardapio na lista
            _context.CardapioItens.Add(cardapioItem);
            _context.SaveChanges();
            return Results.Created($"/api/cardapio/{cardapioItem.Id}", cardapioItem);
        }

        /// <summary>
        /// Atualiza um item do cardápio
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cardapio"></param>
        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] CardapioItemUpdateRequest cardapio)
        {
            var cardapioItem = _context.CardapioItens.FirstOrDefault(c => c.Id == id);

            if (cardapioItem is null)
                return Results.NotFound($"Cardapio do id {id} Nao encontrado");

            if(cardapio.CategoriaCardapioId.HasValue)
            {
                // consulta no banco pelo id da categoria
                var categoria = _context.CategoriaCardapio
                    .FirstOrDefault(c => c.Id == cardapio.CategoriaCardapioId);
                // se o retorno da consulta retornou nulo
                if (categoria is null)
                    return Results.BadRequest("Categoria de cardápio inválida.");
            }
            cardapioItem.Titulo = cardapio.Titulo;
            cardapioItem.Descricao = cardapio.Descricao;
            cardapioItem.Preco = cardapio.Preco;
            cardapioItem.PossuiPreparo = cardapio.PossuiPreparo;

            cardapioItem.CategoriaCardapioId = cardapio.CategoriaCardapioId;

            _context.SaveChanges();
            return Results.NoContent();
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var cardapioItem = _context.CardapioItens
                .FirstOrDefault(c => c.Id == id);
            if (cardapioItem is null)
                return Results.NotFound($"Cardapio {id} não encontrado");
            _context.CardapioItens.Remove(cardapioItem);
            var removido = _context.SaveChanges();
            if (removido>0)
                return Results.NoContent();

            return Results.StatusCode(500);
        }
    }
}
