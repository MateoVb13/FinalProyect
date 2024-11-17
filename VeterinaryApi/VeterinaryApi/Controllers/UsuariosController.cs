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
            // Buscar el usuario con el correo y la contraseña proporcionados
            var usuario = _context.usuarios.FirstOrDefault(u => u.correo_ususario == loginDTO.Email && u.contraseña_usuario == loginDTO.Password);
            if (usuario == null)
            {
                return Unauthorized("Credenciales incorrectas.");
            }

            // Crear las claims que irán dentro del token
            var claims = new[]
            {
        new Claim("id", usuario.idusuarios.ToString()), // ID del usuario
        new Claim(ClaimTypes.Name, usuario.nombre_usuario), // Nombre del usuario
        new Claim(ClaimTypes.Email, usuario.correo_ususario) // Correo electrónico
    };

            // Configuración del token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ClaveSuperSecreta12345678901234567890")); // Reemplaza con tu clave del appsettings.json
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "VeterinaryApi", // Emisor del token
                audience: "VeterinaryApiUsers", // Audiencia
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Token válido por 1 hora
                signingCredentials: creds
            );

            // Generar el token
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            // Agregar el token al objeto Usuario antes de devolverlo
            var usuarioConToken = new
            {
                usuario.idusuarios,
                usuario.nombre_usuario,
                usuario.correo_ususario,
                usuario.telefono_usuario,
                usuario.direccion_usuario,
                usuario.Roles_idroles,
                Token = tokenString,
                Expiration = token.ValidTo
            };

            // Devolver el usuario con el token
            return Ok(usuarioConToken);
        }




    }
}
