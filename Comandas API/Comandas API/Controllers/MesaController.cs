using Comandas_API.DTOS;
using Comandas_API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {

        public List<Mesa> mesas = new List<Mesa>()
        {
            new Mesa
            {
                Id = 1,
                NumeroMesa = 1,
                SituacaoMesa = (int)SituacaoMesa.Livre
            },
            new Mesa
            {
               Id = 2,
               NumeroMesa = 2,
               SituacaoMesa = (int)SituacaoMesa.Ocupada
            }
        };
        // GET: api/<MesaController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MesaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {

            var mesa = mesas.FirstOrDefault(m => m.Id == id);
            if (mesa is null)
            {
                return Results.NotFound("Mesa não encontrada");
            }
            return Results.Ok(mesa);
        }
        // POST api/<MesaController>
        [HttpPost]
        public IResult Post([FromBody] MesaCreateRequest mesaCreate)
        {
            var novaMesa = new Mesa
            {
                Id = mesas.Count + 1,
                NumeroMesa = mesaCreate.NumeroMesa,
                SituacaoMesa = mesaCreate.SituacaoMesa
            };
            //adicionar a nova mesa na lista
            mesas.Add(novaMesa);
            //retornar a nova mesa criada e o codigo 201 CREATED
            return Results.Created($"/api/mesa/{novaMesa.Id}", novaMesa);
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] MesaCreateUpdateRequest mesaUpdate)
        {
          if(mesaUpdate.NumeroMesa <= 0)
           
                Results.BadRequest("O número da mesa deve ser maior que zero.");

          if(mesaUpdate.SituacaoMesa < 0 || mesaUpdate.SituacaoMesa > 2)

                Results.BadRequest("A situação da mesa deve ser 0 (Livre), 1 (Ocupada) ou 2 (Reservada).");
          var mesa = mesas.FirstOrDefault(m => m.Id == id);
            if (mesa is null)
                return Results.NotFound($"Mesa {id} não encontrada.");
            mesa.NumeroMesa = mesaUpdate.NumeroMesa;
            mesa.SituacaoMesa = mesaUpdate.SituacaoMesa;
            return Results.NoContent();
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
