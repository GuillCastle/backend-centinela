namespace Backend.DTOs.Producto
{
    public class ProductoIdDTO
    {
        public long Codigo { get; set; }

        public string CodBusqueda { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public int Marca { get; set; }

        public int UnidadMedida { get; set; }

        public int Estado { get; set; }

        public int AgenteEconomico { get; set; }
        public string DescripcionSAT { get; set; }
        public string CodBarras     { get; set; }
    }
}
