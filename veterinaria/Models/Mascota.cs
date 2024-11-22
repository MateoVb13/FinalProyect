using System.Text.Json.Serialization;

namespace veterinaria.Models
{

    public class Mascota
    {
<<<<<<< HEAD
        
        public int Idmascotas { get; set; }

        
        public required string NombreMascota { get; set; }

        
        public int EdadMascota { get; set; }

        
        public DateTime FechaNacimiento { get; set; }

        
        public int ClientesIdclientes { get; set; }

        
        public required string TipoAnimal { get; set; }

        
        public required string RazaAnimal { get; set; }

        
        public int UsuariosDuenoIdusuarios { get; set; }
=======
        public int idmascotas { get; set; }
        public string nombre_mascota { get; set; }
        public int edad_mascota { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string tipo_animal { get; set; }
        public string raza_animal { get; set; }
        public int usuarios_dueno_idusuarios { get; set; }
>>>>>>> dc8d39abb04c5befaf53ec02e3d7c4a7320bf13b
    }

}


