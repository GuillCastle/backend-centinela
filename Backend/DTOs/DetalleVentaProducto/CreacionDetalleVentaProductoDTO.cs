namespace Backend.DTOs.DetalleVentaProducto
{
    public class CreacionDetalleVentaProductoDTO
    {
        public long Producto { get; set; }

        public long Venta { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioCosto { get; set; }

        public decimal PrecioVenta { get; set; }

        public decimal Monto { get; set; }

        public decimal Iva { get; set; }

        public decimal TotalSinIva { get; set; }
    }
}
