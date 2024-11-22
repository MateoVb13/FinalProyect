using System.Text.Json.Serialization;

namespace veterinaria.Models
{
    public class Mascota
    {
        
        public int Idmascotas { get; set; }

        
        public required string NombreMascota { get; set; }

        
        public int EdadMascota { get; set; }

        
        public DateTime FechaNacimiento { get; set; }

        
        public int ClientesIdclientes { get; set; }

        
        public required string TipoAnimal { get; set; }

        
        public required string RazaAnimal { get; set; }

        
        public int UsuariosDuenoIdusuarios { get; set; }
    }
}
