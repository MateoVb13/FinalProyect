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

        [HttpPost]
        public ActionResult<Mascota> CreateMascota(Mascota mascota)
        {
            _context.mascotas.Add(mascota);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetMascota), new { id = mascota.idmascotas }, mascota);
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
