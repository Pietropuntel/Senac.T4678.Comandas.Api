using Comandas_API.DTOS;
using Comandas_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {

        public ComandasDbContext _context { get; set; }
        public MesaController(ComandasDbContext context)
        {
            _context = context;
        }
       
     
        // GET: api/<MesaController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(_context.Mesas.ToList());
    }

        // GET api/<MesaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
    {

            var mesa = _context.Mesas.FirstOrDefault(m => m.Id == id);
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
                
                NumeroMesa = mesaCreate.NumeroMesa,
                SituacaoMesa = mesaCreate.SituacaoMesa
            };
            //adicionar a nova mesa na lista
            _context.Add(novaMesa);
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
          var mesa = _context.Mesas.FirstOrDefault(m => m.Id == id);
            if (mesa is null)
                return Results.NotFound($"Mesa {id} não encontrada.");
            mesa.NumeroMesa = mesaUpdate.NumeroMesa;
            mesa.SituacaoMesa = mesaUpdate.SituacaoMesa;
            return Results.NoContent();
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var mesa = _context.Mesas
               .FirstOrDefault(c => c.Id == id);
            if (mesa is null)
                return Results.NotFound("pedido não encontrado");
            var removidoComSucesso = _context.Remove(mesa);

            return Results.Ok("Mesa removida com sucesso");
        }
    }
}
