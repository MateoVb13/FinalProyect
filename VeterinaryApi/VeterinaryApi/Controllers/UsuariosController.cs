using Microsoft.AspNetCore.Mvc;
using VeterinaryApi.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


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

        [HttpPut("Actualizar")]
        public IActionResult UpdateCurrentUser([FromBody] Usuario usuarioActualizado)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized("Usuario no autenticado.");
            }

            var usuario = _context.usuarios.FirstOrDefault(u => u.idusuarios == userId);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            // Actualizar los datos del usuario
            usuario.nombre_usuario = usuarioActualizado.nombre_usuario;
            usuario.correo_ususario = usuarioActualizado.correo_ususario;
            usuario.direccion_usuario = usuarioActualizado.direccion_usuario;
            usuario.telefono_usuario = usuarioActualizado.telefono_usuario;

            // Actualizar la contraseña solo si se proporcionó
            if (!string.IsNullOrWhiteSpace(usuarioActualizado.contraseña_usuario))
            {
                usuario.contraseña_usuario = usuarioActualizado.contraseña_usuario;
            }

            _context.Entry(usuario).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }



        // Endpoint para registrar un nuevo usuario
        [HttpPost("register")]
        public ActionResult Register([FromBody] Usuario usuario)
        {
            try
            {
                if (_context.usuarios.Any(u => u.correo_ususario == usuario.correo_ususario))
                {
                    Console.WriteLine("Ya existe un usuario con el correo: " + usuario.correo_ususario);
                    return BadRequest("Ya existe un usuario con este correo.");
                }

                _context.usuarios.Add(usuario);
                _context.SaveChanges();
                Console.WriteLine("Usuario registrado exitosamente: " + usuario.nombre_usuario);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar usuario: {ex.Message}");
                return StatusCode(500, $"Error al registrar usuario: {ex.Message}");
            }

            return Ok("Usuario registrado exitosamente");
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] UsuarioLoginDTO loginDTO)
        {
            var usuario = _context.usuarios.FirstOrDefault(u => u.correo_ususario == loginDTO.Email && u.contraseña_usuario == loginDTO.Password);
            if (usuario == null)
            {
                return Unauthorized("Credenciales incorrectas.");
            }

            var claims = new[]
            {
                new Claim("id", usuario.idusuarios.ToString()),
                new Claim(ClaimTypes.Name, usuario.nombre_usuario),
                new Claim(ClaimTypes.Email, usuario.correo_ususario)
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ClaveSuperSecreta12345678901234567890"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "VeterinaryApi",
                audience: "VeterinaryApiUsers",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                Token = tokenString,
                usuario.nombre_usuario,
                usuario.correo_ususario
            });
        }

        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized("Usuario no autenticado.");
            }

            var usuario = _context.usuarios.FirstOrDefault(u => u.idusuarios == userId);
            if (usuario == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            return Ok(new
            {
                usuario.nombre_usuario,
                usuario.correo_ususario
            });
        }




    }
}
