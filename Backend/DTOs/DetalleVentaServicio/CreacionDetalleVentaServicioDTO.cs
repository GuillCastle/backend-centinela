namespace Backend.DTOs.DetalleVentaServicio
{
    public class CreacionDetalleVentaServicioDTO
    {
        public long Codigo { get; set; }

        public long Venta { get; set; }

        public int Cantidad { get; set; }

        public string Descripcion { get; set; } = null!;

        public DateTime? PeriodoDesde { get; set; }

        public DateTime? PeriodoHasta { get; set; }

        public decimal Monto { get; set; }

        public decimal Iva { get; set; }

        public decimal TotalSinIva { get; set; }
    }
}
