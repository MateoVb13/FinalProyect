using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using VeterinaryApi.Models;

namespace VeterinaryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoAtencionController : ControllerBase
    {
        private readonly VeterinaryContext _context;

        public TipoAtencionController(VeterinaryContext context)
        {
            _context = context;
        }

        // Endpoint para obtener solo los nombres de los tipos de atención
        [HttpGet("nombres")]
        public ActionResult<IEnumerable<string>> GetTipoAtencionNombres()
        {
            try
            {
                // Seleccionar únicamente los nombres
                var nombres = _context.tipo_atencion_o_servicio
                    .Select(t => t.nombre_tipo)
                    .ToList();

                // Retornar los nombres
                return Ok(nombres);
            }
            catch (Exception ex)
            {
                // Manejar errores y retornar un mensaje de error
                return StatusCode(500, $"Error al obtener tipos de atención: {ex.Message}");
            }
        }
    }
}
