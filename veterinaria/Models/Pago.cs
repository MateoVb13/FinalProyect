using System.Text.Json.Serialization;

namespace veterinaria.Models
{
    public class Pago
    {
        [JsonPropertyName("iddetalle_pagos")]
        public int IddetallePagos { get; set; }

        [JsonPropertyName("fecha_hora_pago")]
        public DateTime FechaHoraPago { get; set; }

        [JsonPropertyName("monto_pago")]
        public decimal MontoPago { get; set; }

        [JsonPropertyName("estados_pago_idestados_pago")]
        public int EstadosPagoIdestadosPago { get; set; }

        [JsonPropertyName("tipos_pago_idtipo_pago")]
        public int TiposPagoIdtipoPago { get; set; }

        [JsonPropertyName("facturas_idfacturas")]
        public int FacturasIdfacturas { get; set; }
    }
}
