using System.Text.Json.Serialization;

namespace veterinaria.Models
{
    public class Pago
    {
        
        public int IddetallePagos { get; set; }

        
        public DateTime FechaHoraPago { get; set; }

        
        public decimal MontoPago { get; set; }

        
        public int EstadosPagoIdestadosPago { get; set; }

        
        public int TiposPagoIdtipoPago { get; set; }

        
        public int FacturasIdfacturas { get; set; }
    }
}
