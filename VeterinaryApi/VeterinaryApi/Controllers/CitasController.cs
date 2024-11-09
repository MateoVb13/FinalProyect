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

        [HttpGet("{id}")]
        public ActionResult<AtencionServicio> GetCita(int id)
        {
            var cita = _context.atenciones_y_servicios.FirstOrDefault(c => c.idcitas == id);
            if (cita == null)
            {
                return NotFound();
            }
            return cita;
        }

        [HttpPost]
        public ActionResult<AtencionServicio> CreateCita(AtencionServicio cita)
        {
            _context.atenciones_y_servicios.Add(cita);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCita), new { id = cita.idcitas }, cita);
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
