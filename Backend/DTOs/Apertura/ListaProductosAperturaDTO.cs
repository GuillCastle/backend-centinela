namespace Backend.DTOs.Apertura
{
    public class ListaProductosAperturaDTO
    {
        public int Producto { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public string Marca {  get; set; }
        public string UnidadMedida { get; set; }
        public int Apertura { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta {  get; set; }
        public decimal TotalCosto { get; set; }
        public decimal TotalVenta { get; set; }
    }
}
