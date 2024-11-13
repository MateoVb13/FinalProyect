using System.Text.Json.Serialization;

namespace veterinaria.Models
{
    public class Usuario
    {
        [JsonPropertyName("idusuarios")]
        public int Idusuarios { get; set; }

        [JsonPropertyName("nombre_usuario")]
        public required string NombreUsuario { get; set; }

        [JsonPropertyName("correo_usuario")]
        public required string CorreoUsuario { get; set; }

        [JsonPropertyName("telefono_usuario")]
        public required string TelefonoUsuario { get; set; }

        [JsonPropertyName("direccion_usuario")]
        public required string DireccionUsuario { get; set; }

        [JsonPropertyName("contraseña_usuario")]
        public required string ContraseñaUsuario { get; set; }

        [JsonPropertyName("roles_idroles")]
        public int RolesIdroles { get; set; }
    }
}
