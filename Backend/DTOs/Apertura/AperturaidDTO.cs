namespace Backend.DTOs.Apertura
{
    public class AperturaidDTO
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public string Documento { get; set; }
        public int Estado { get; set; }
        public List<ListaProductosAperturaDTO> Productos { get; set; }
    }
}
