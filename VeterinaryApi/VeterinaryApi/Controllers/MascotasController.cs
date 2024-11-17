﻿using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<Mascota> RegistrarMascota([FromBody] RegistrarMascotaDTO mascotaDTO)
        {
            try
            {
                // Obtener el ID del usuario autenticado desde los claims
                var userIdClaim = User.FindFirst("id");
                if (userIdClaim == null)
                {
                    return Unauthorized("Usuario no autenticado.");
                }
                var userId = int.Parse(userIdClaim.Value);

                // Crear la nueva mascota
                var nuevaMascota = new Mascota
                {
                    nombre_mascota = mascotaDTO.nombre_mascota,
                    edad_mascota = mascotaDTO.edad_mascota,
                    fecha_nacimiento = mascotaDTO.fecha_nacimiento,
                    tipo_animal = mascotaDTO.tipo_animal,
                    raza_animal = mascotaDTO.raza_animal,
                    usuarios_dueno_idusuarios = userId
                };

                _context.mascotas.Add(nuevaMascota);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetMascota), new { id = nuevaMascota.idmascotas }, nuevaMascota);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
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
