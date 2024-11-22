using System;
using System.Text.Json.Serialization;

namespace veterinaria.Models
{
    public class Cita
    {
        
        public int Idcitas { get; set; }

        
        public DateTime FechaApartada { get; set; }

        
        public TimeSpan HoraInicio { get; set; }

        
        public TimeSpan HoraFinal { get; set; }

        
        public int EstadosCitaIdestadosCita { get; set; }

        
        public int TipoAtencionOServicioIdatencionOServicio { get; set; }

        
        public int UsuariosVeterinarioIdusuarios { get; set; }

        
        public int MascotasIdmascotas { get; set; }
    }
}
