namespace Backend.DTOs.Activo
{
    public class ActivoDTO
    {
        public int Codigo { get; set; }

        public DateTime FechaCompra { get; set; }

        public string NoFactura { get; set; } = null!;

        public string SerieFactura { get; set; } = null!;

        public string DescripcionBien { get; set; } = null!;

        public decimal TasaDepreciacion { get; set; }

        public decimal ValorActivo { get; set; }

        public decimal ValorInicial { get; set; }

        public decimal VidaUtil { get; set; }

        public decimal ValorLibros { get; set; }

        public int Entidad { get; set; }

        public int Sucursal { get; set; }

        public int Usuario { get; set; }

        public DateTime FechaRegistro { get; set; }

        public int? CodigoReferencia { get; set; }

        public int? TipoMovimiento { get; set; }

        public int? Estado { get; set; }
    }
}
