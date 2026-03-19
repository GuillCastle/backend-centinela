namespace Backend.DTOs.DetalleVentaProducto
{
    public class DetalleVentaProductoDTO
    {
        public long Producto { get; set; }

        public string DescripcionProducto { get; set; }

        public long Venta { get; set; }

        public int Cantidad { get; set; }

        public int Existencia { get; set; }

        public decimal PrecioCosto { get; set; }

        public decimal PrecioVenta { get; set; }

        public decimal Monto { get; set; }

        public decimal Iva { get; set; }

        public decimal TotalSinIva { get; set; }
    }
}
