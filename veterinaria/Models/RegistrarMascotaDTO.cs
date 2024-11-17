using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace veterinaria.Models
{
    public class RegistrarMascotaDTO
    {
        public string nombre_mascota { get; set; }
        public int edad_mascota { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string tipo_animal { get; set; }
        public string raza_animal { get; set; }
        public int usuarios_dueno_idusuarios { get; set; }
    }
}
