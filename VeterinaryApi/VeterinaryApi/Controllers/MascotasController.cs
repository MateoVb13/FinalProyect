using Microsoft.AspNetCore.Mvc;
using VeterinaryApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace VeterinaryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MascotasController : ControllerBase
    {
        private readonly VeterinaryContext _context;

        public MascotasController(VeterinaryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Mascota>> GetMascotas()
        {
            return _context.mascotas.ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<Mascota> GetMascota(int id)
        {
            var mascota = _context.mascotas.FirstOrDefault(m => m.idmascotas == id);
            if (mascota == null)
            {
                return NotFound();
            }
            return mascota;
        }

        [HttpPost("register")]
        public IActionResult RegisterMascota([FromBody] RegistrarMascotaDTO mascota)
        {
            try
            {
                // Extraer el userId desde el token JWT
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
                {
                    return Unauthorized("Usuario no autenticado.");
                }

                // Asignar el userId extraído al DTO
                mascota.usuarios_dueno_idusuarios = userId;

                // Validar los datos de la mascota
                if (string.IsNullOrWhiteSpace(mascota.nombre_mascota) ||
                    string.IsNullOrWhiteSpace(mascota.tipo_animal) ||
                    string.IsNullOrWhiteSpace(mascota.raza_animal) ||
                    mascota.edad_mascota <= 0)
                {
                    return BadRequest("Datos inválidos para el registro de la mascota.");
                }

                // Guardar la mascota en la base de datos
                var nuevaMascota = new Mascota
                {
                    nombre_mascota = mascota.nombre_mascota,
                    edad_mascota = mascota.edad_mascota,
                    fecha_nacimiento = mascota.fecha_nacimiento,
                    tipo_animal = mascota.tipo_animal,
                    raza_animal = mascota.raza_animal,
                    usuarios_dueno_idusuarios = userId
                };

                _context.mascotas.Add(nuevaMascota);
                _context.SaveChanges();

                return Ok("Mascota registrada exitosamente.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al registrar mascota: {ex.Message}");
                return StatusCode(500, "Ocurrió un error al registrar la mascota.");
            }
        }



        [HttpPut("{id}")]
        public IActionResult UpdateMascota(int id, Mascota mascota)
        {
            if (id != mascota.idmascotas)
            {
                return BadRequest();
            }

            _context.Entry(mascota).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMascota(int id)
        {
            var mascota = _context.mascotas.FirstOrDefault(m => m.idmascotas == id);
            if (mascota == null)
            {
                return NotFound();
            }

            _context.mascotas.Remove(mascota);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
