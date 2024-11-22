using System;
using System.Text.Json.Serialization;

namespace veterinaria.Models
{
    public class AtencionServicio
    {
        [JsonPropertyName("idcitas")]
        public int Idcitas { get; set; }

        [JsonPropertyName("fecha_apartada")]
        public DateTime FechaApartada { get; set; }

        [JsonPropertyName("hora_inicio")]
        public TimeSpan HoraInicio { get; set; }

        [JsonPropertyName("hora_final")]
        public TimeSpan HoraFinal { get; set; }

        [JsonPropertyName("estados_cita_idestados_cita")]
        public int EstadosCitaIdestadosCita { get; set; }

        [JsonPropertyName("tipo_atencion_o_servicio_idatencion_o_servicio")]
        public int TipoAtencionOServicioIdatencionOServicio { get; set; }

        [JsonPropertyName("usuarios_veterinario_idusuarios")]
        public int UsuariosVeterinarioIdusuarios { get; set; }

        [JsonPropertyName("mascotas_idmascotas")]
        public int MascotasIdmascotas { get; set; }
    }
}
