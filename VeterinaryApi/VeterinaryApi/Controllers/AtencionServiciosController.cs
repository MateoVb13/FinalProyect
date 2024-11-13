using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VeterinaryApi.Context;
using VeterinaryApi.Models;

namespace VeterinaryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtencionServiciosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AtencionServiciosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AtencionServicios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AtencionServicio>>> GetCitas()
        {
            return await _context.atenciones_y_servicios.ToListAsync();
        }

        // GET: api/AtencionServicios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AtencionServicio>> GetAtencionServicio(int id)
        {
            var atencionServicio = await _context.atenciones_y_servicios.FindAsync(id);

            if (atencionServicio == null)
            {
                return NotFound();
            }

            return atencionServicio;
        }

        // PUT: api/AtencionServicios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAtencionServicio(int id, AtencionServicio atencionServicio)
        {
            if (id != atencionServicio.idcitas)
            {
                return BadRequest();
            }

            _context.Entry(atencionServicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AtencionServicioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/AtencionServicios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AtencionServicio>> PostAtencionServicio(AtencionServicio atencionServicio)
        {
            _context.atenciones_y_servicios.Add(atencionServicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAtencionServicio", new { id = atencionServicio.idcitas }, atencionServicio);
        }

        // DELETE: api/AtencionServicios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAtencionServicio(int id)
        {
            var atencionServicio = await _context.atenciones_y_servicios.FindAsync(id);
            if (atencionServicio == null)
            {
                return NotFound();
            }

            _context.atenciones_y_servicios.Remove(atencionServicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AtencionServicioExists(int id)
        {
            return _context.atenciones_y_servicios.Any(e => e.idcitas == id);
        }
    }
}
