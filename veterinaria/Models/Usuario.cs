using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace veterinaria.Models
{
    public class Usuario
    {
        [JsonPropertyName("idusuarios")]
        public int Idusuarios { get; set; }

        [JsonPropertyName("nombre_usuario")]
        public required string nombre_usuario { get; set; }

        [JsonPropertyName("correo_ususario")]
        public required string correo_ususario { get; set; }

        [JsonPropertyName("telefono_usuario")]
        public required string telefono_usuario { get; set; }

        [JsonPropertyName("direccion_usuario")]
        public required string direccion_usuario { get; set; }

        [JsonPropertyName("contraseña_usuario")]
        public required string contraseña_usuario { get; set; }

        [JsonPropertyName("roles_idroles")]
        public int Roles_idroles { get; set; }
    }
}

