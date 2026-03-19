namespace Backend.DTOs.Ventas.Venta
{
    public class creacionVentaDTO
    {
        public int Cliente { get; set; }

        public string NombreConsumidorFinal { get; set; } = null!;

        public string NumeroDocumento { get; set; } = null!;

        public string? SerieDocumento { get; set; }

        public DateTime? FechaDocumento { get; set; }

        public string? Uuid { get; set; }

        public string? Xml { get; set; }

        public decimal TotalDocumento { get; set; }

        public decimal Iva { get; set; }

        public decimal TotalSinIva { get; set; }

        public DateTime FechaRegistro { get; set; }

        public int UsuarioRegistro { get; set; }

        public DateTime? FechaCaja { get; set; }

        public int? UsuarioCaja { get; set; }

        public int Entidad { get; set; }

        public int Sucursal { get; set; }

        public int Estado { get; set; }

        public int? Anio { get; set; }

        public int? CorteCaja { get; set; }
    }
}
