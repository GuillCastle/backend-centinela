namespace Backend.DTOs.DetalleCompraProducto
{
    public class DetalleCompraProductoDTO
    {
        public long Producto { get; set; }

        public long Compra { get; set; }

        public string DescripcionProducto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioCosto { get; set; }

        public decimal PrecioVenta { get; set; }

        public decimal Monto { get; set; }

        public int CantidadFaltante { get; set; }

        public decimal Iva { get; set; }

        public decimal TotalSinIva { get; set; }
    }
}
