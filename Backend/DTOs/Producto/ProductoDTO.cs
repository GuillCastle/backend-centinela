namespace Backend.DTOs.Producto
{
    public class ProductoDTO
    {
        public long Codigo { get; set; }

        public string CodBusqueda { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public string Marca { get; set; }

        public string UnidadMedida { get; set; }

        public string Estado { get; set; }

        public string AgenteEconomico { get; set; }
        public string DescripcionSAT { get; set; }
    }
}
