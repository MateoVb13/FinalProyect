using Microsoft.AspNetCore.Mvc;
using VeterinaryApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace VeterinaryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly VeterinaryContext _context;

        public CitasController(VeterinaryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AtencionServicio>> GetCitas()
        {
            return _context.atenciones_y_servicios.ToList();
        }

        [HttpGet("mis-citas")]
        public IActionResult GetCitasUsuario()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized("Usuario no autenticado.");
            }

            var citas = _context.atenciones_y_servicios.Where(c => c.mascotas_idmascotas == userId).ToList();
            return Ok(citas);
        }


        [HttpPost]
        public IActionResult AgendarCita([FromBody] AtencionServicio cita)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
            {
                return Unauthorized("Usuario no autenticado.");
            }

            // Validar conflictos de horario
            var conflicto = _context.atenciones_y_servicios.Any(c =>
                c.fecha_apartada == cita.fecha_apartada &&
                c.hora_inicio < cita.hora_final &&
                c.hora_final > cita.hora_inicio);

            if (conflicto)
            {
                return BadRequest("El horario está en conflicto con otra cita.");
            }

            // Asignar automáticamente el estado como 'Pendiente'
            cita.estados_cita_idestados_cita = 1;

            // Registrar la cita
            _context.atenciones_y_servicios.Add(cita);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetCitas), new { id = cita.idcitas }, cita);
        }



        [HttpPut("{id}")]
        public IActionResult UpdateCita(int id, AtencionServicio cita)
        {
            if (id != cita.idcitas)
            {
                return BadRequest();
            }

            _context.Entry(cita).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCita(int id)
        {
            var cita = _context.atenciones_y_servicios.FirstOrDefault(c => c.idcitas == id);
            if (cita == null)
            {
                return NotFound();
            }

            _context.atenciones_y_servicios.Remove(cita);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
