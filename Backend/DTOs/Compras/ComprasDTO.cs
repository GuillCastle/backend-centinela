namespace Backend.DTOs.Compras
{
    public class ComprasDTO
    {
        public long Codigo { get; set; }

        public string Proveedor { get; set; }

        public string NumeroDocumento { get; set; } = null!;

        public string SerieDocumento { get; set; } = null!;

        public string FechaDocumento { get; set; }

        public string? Uuid { get; set; }

        public string TotalDocumento { get; set; }

        public string? RetencionIva { get; set; }

        public string? UrlretencionIva { get; set; }

        public string? RetencionIsr { get; set; }

        public string? UrlretencionIsr { get; set; }

        public string TotalPagar { get; set; }

        public string Iva { get; set; }

        public string Exento { get; set; }

        public string TotalSinIva { get; set; }

        public string FechaRegistro { get; set; }

        public string UsuarioRegistro { get; set; }

        public DateTime? FechaAprobacion { get; set; }

        public string? UsuarioAprobacion { get; set; }

        public DateTime? FechaRevision { get; set; }

        public string? UsuarioRevision { get; set; }

        public string Entidad { get; set; }

        public string Sucursal { get; set; }

        public string Estado { get; set; }

        public string? Anio { get; set; }

        public string? FacturaEspecial { get; set; }
    }
}
