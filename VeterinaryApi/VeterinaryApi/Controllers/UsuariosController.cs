using Microsoft.AspNetCore.Mvc;
using VeterinaryApi.Models;
using System.Collections.Generic;
using System.Linq;



namespace VeterinaryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        
        private readonly VeterinaryContext _context;


        public UsuariosController(VeterinaryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Usuario>> GetUsuarios()
        {
            return _context.usuarios.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> GetUsuario(int id)
        {
            var usuario = _context.usuarios.FirstOrDefault(u => u.idusuarios == id);
            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }


        [HttpPost]
        public ActionResult<Usuario> CreateUsuario(Usuario usuario)
        {
            _context.usuarios.Add(usuario);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.idusuarios }, usuario);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, Usuario usuario)
        {
            if (id != usuario.idusuarios)
            {
                return BadRequest();
            }

            _context.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            var usuario = _context.usuarios.FirstOrDefault(u => u.idusuarios == id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.usuarios.Remove(usuario);
            _context.SaveChanges();

            return NoContent();
        }


        // Endpoint para registrar un nuevo usuario
        [HttpPost("register")]
        public ActionResult<Usuario> Register([FromBody] Usuario usuario)
        {
            // Verificar si ya existe un usuario con el mismo correo
            if (_context.usuarios.Any(u => u.correo_ususario == usuario.correo_ususario))
            {
                return BadRequest("Ya existe un usuario con este correo.");
            }

            // Agregar el nuevo usuario
            _context.usuarios.Add(usuario);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.idusuarios }, usuario);
        }



        // Endpoint para iniciar sesión
        [HttpPost("login")]
        public ActionResult<Usuario> Login([FromBody] UsuarioLoginDTO loginDTO)
        {
            // Buscar el usuario con el correo y la contraseña proporcionados
            var usuario = _context.usuarios.FirstOrDefault(u => u.correo_ususario == loginDTO.Email && u.contraseña_usuario == loginDTO.Password);
            if (usuario == null)
            {
                return Unauthorized("Credenciales incorrectas.");
            }

            return Ok(usuario);
        }


    }
}
