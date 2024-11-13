using System.Text.Json.Serialization;

namespace veterinaria.Models
{
    public class Factura
    {
        [JsonPropertyName("idfacturas")]
        public int Idfacturas { get; set; }

        [JsonPropertyName("usuarios_idusuarios")]
        public int UsuariosIdusuarios { get; set; }
    }
}
