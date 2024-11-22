using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace veterinaria.Models
{
    public class Usuario
    {
        
        public int idusuarios { get; set; }

        
        public required string nombre_usuario { get; set; }

        
        public required string correo_ususario { get; set; }

        
        public required string telefono_usuario { get; set; }

        
        public required string direccion_usuario { get; set; }

        
        public required string contraseña_usuario { get; set; }
        
        
        public int Roles_idroles { get; set; }

        public string Token { get; set; }
    }
}
