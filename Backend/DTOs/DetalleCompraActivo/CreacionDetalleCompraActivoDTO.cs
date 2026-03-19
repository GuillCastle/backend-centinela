namespace Backend.DTOs.DetalleCompraActivo
{
    public class CreacionDetalleCompraActivoDTO
    {
        public long Compra { get; set; }

        public string DescripcionBien { get; set; } = null!;

        public decimal TasaDepreciacion { get; set; }

        public decimal ValorActivo { get; set; }

        public decimal ValorInicial { get; set; }

        public decimal VidaUtil { get; set; }

        public decimal ValorLibros { get; set; }

        public decimal ValorSinIva { get; set; }

        public decimal Iva { get; set; }

        public int Entidad { get; set; }

        public int Sucursal { get; set; }

        public int Usuario { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
