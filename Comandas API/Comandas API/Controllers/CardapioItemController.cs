using Comandas_API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]// Define que essa classe é um controlador de API
    public class CardapioItemController : ControllerBase// Define que essa classe herda de ControllerBase
    {
        public List<CardapioItem> cardapios = new List<CardapioItem>()
        {
        new CardapioItem
        {
            Id = 1,
            Titulo = "Coxinha",
            Descricao = "Coxinha de frango com catupiry",
            Preco = 5.00M,
            PossuiPreparo = true
        },
        new CardapioItem// Define um novo item de cardápio
          {
                Id = 2,
                Titulo = "X-LasVegas",
                Descricao = "Areia e Carne",
                Preco = 25.00M,
                PossuiPreparo = true
          }

        };
        //Metodo GET que retorna uma lista de cardapio
        // GET: api/<CardapioItemController>
        [HttpGet]// Define que esse método responde a requisições GET
        public IEnumerable<CardapioItem> Get()
        {
            //Cria uma lista de cardapio
            return new CardapioItem[]
            {
                 new CardapioItem
                {
                    Id = 1,
                    Titulo = "Coxinha",
                    Descricao = "Coxinha de frango com catupiry",
                    Preco = 5.00M,
                    PossuiPreparo = true
                },
                   new CardapioItem// Define um novo item de cardápio
                {
                    Id = 2,
                    Titulo = "X-LasVegas",
                    Descricao = "Areia e Carne",
                    Preco = 25.00M,
                    PossuiPreparo = true
                },
            };
        }
       
        // GET api/<CardapioItemController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var cardapio = cardapios.FirstOrDefault(c => c.Id == id);
            if(cardapio is null)
            {
                return Results.NotFound("Cardapio não encontrado!");
            }
            return Results.Ok(cardapio);
        }

        // POST api/<CardapioItemController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
