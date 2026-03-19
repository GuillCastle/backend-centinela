namespace Backend.DTOs.DetalleCompraActivo
{
    public class DetalleCompraActivoDTO
    {
        public long Compra { get; set; }

        public long Codigo { get; set; }

        public string DescripcionBien { get; set; } = null!;

        public decimal TasaDepreciacion { get; set; }

        public decimal ValorActivo { get; set; }

        public decimal ValorInicial { get; set; }

        public decimal VidaUtil { get; set; }

        public decimal ValorLibros { get; set; }

        public decimal ValorSinIva { get; set; }

        public decimal Iva { get; set; }

    }
}
