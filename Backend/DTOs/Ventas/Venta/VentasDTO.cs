namespace Backend.DTOs.Ventas.Venta
{
    public class VentasDTO
    {
        public long Codigo { get; set; }

        public string Cliente { get; set; } = null!;

        public string NombreConsumidorFinal { get; set; } = null!;

        public string NumeroDocumento { get; set; } = null!;

        public string SerieDocumento { get; set; } = null!;

        public string FechaDocumento { get; set; }

        public decimal TotalDocumento { get; set; }

        public decimal Iva { get; set; }

        public decimal TotalSinIva { get; set; }

        public string Estado { get; set; } = null!;

    }
}
