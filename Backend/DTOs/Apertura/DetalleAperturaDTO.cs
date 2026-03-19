namespace Backend.DTOs.Apertura
{
    public class DetalleAperturaDTO
    {
        public long Producto { get; set; }

        public int Apertura { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioCosto { get; set; }

        public decimal PrecioVenta { get; set; }
    }
}
