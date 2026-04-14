namespace Backend.DTOs.Mapas
{
    public class DepartamentoMapaDTO
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string? Wkt { get; set; }
    }
}
