namespace Backend.DTOs.Mapas
{
    public class EventoMapaDTO
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public string? Wkt { get; set; }
        public int Estado { get; set; }
    }
}
