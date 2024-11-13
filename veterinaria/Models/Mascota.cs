using System.Text.Json.Serialization;

namespace veterinaria.Models
{
    public class Mascota
    {
        [JsonPropertyName("idmascotas")]
        public int Idmascotas { get; set; }

        [JsonPropertyName("nombre_mascota")]
        public required string NombreMascota { get; set; }

        [JsonPropertyName("edad_mascota")]
        public int EdadMascota { get; set; }

        [JsonPropertyName("fecha_nacimiento")]
        public DateTime FechaNacimiento { get; set; }

        [JsonPropertyName("clientes_idclientes")]
        public int ClientesIdclientes { get; set; }

        [JsonPropertyName("tipo_animal")]
        public required string TipoAnimal { get; set; }

        [JsonPropertyName("raza_animal")]
        public required string RazaAnimal { get; set; }

        [JsonPropertyName("usuarios_dueno_idusuarios")]
        public int UsuariosDuenoIdusuarios { get; set; }
    }
}
