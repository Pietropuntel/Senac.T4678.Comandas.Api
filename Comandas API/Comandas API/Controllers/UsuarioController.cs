using Comandas_API.DTOS;
using Comandas_API.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        static List<Usuario> usuarios = new List<Usuario>() { 
            new Usuario
            {
                Id = 1,
                Nome = "Admin",
                Email = "admin@admin.com",
                Senha = "admin123"
            },        
           new Usuario
           {
               Id = 2,
               Nome = "Usuario",
               Email = "usuario@usuario.com",
               Senha = "usuario123"
           }
        
        
        
        };
        // GET: api/<UsuarioController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(usuarios);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario is null)
            {
                return Results.NotFound("Usuario não encontrado");
            }
            return Results.Ok(usuario);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public IResult Post ([FromBody] UsuarioCreateRequest usuarioCreate)
        {
            if(usuarioCreate.Senha.Length < 6)
            {
                return Results.BadRequest("A senha deve ter no mínimo 6 caracteres");
            }
            if(usuarioCreate.Nome.Length < 3)
            {
                return Results.BadRequest("O nome deve ter no mínimo 3 caracteres");
            }
            if(usuarioCreate.Email.Length < 5 || !usuarioCreate.Email.Contains("@"))
            {
                return Results.BadRequest("O email deve ter no mínimo 5 caracteres e conter @");
            }
            var usuario = new Usuario
            {
                Id = usuarios.Count + 1,
                Nome = usuarioCreate.Nome,
                Email = usuarioCreate.Email,
                Senha = usuarioCreate.Senha
            };
            //add o usuario na lista
            usuarios.Add(usuario);
            return Results.Created($"/api/usuario/{usuario.Id}", usuario);
        }
        //Put api/<UsuarioController>/5
        //<summary>
        //Atualizar um usuario
        //</summary>
        //<param name="id">Id do usuario</param>
        //<param name="usuarioUpdate">Objeto com os dados para atualizar</param>
        //<returns></returns>
        // PUT api/<UsuarioController>/5 
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] UsuarioUpdateRequest usuarioUpdate)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario is null)
                return Results.NotFound($"Usuario do id {id} Nao encontrado");
              return Results.NoContent();
            usuario.Nome = usuarioUpdate.Nome;
            usuario.Email = usuarioUpdate.Email;
            usuario.Senha = usuarioUpdate.Senha;
            //retorna no content
            return Results.NoContent();
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            var Usuario = usuarios
               .FirstOrDefault(c => c.Id == id);
            if (usuarios is null)
                return Results.NotFound("pedido não encontrado");
            var removidoComSucesso = usuarios.Remove(Usuario);
            if (removidoComSucesso)
                return Results.NotFound();
            return Results.StatusCode(500);
        }
    }
}
