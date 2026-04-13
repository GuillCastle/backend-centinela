namespace Backend.DTOs.Evento
{
    public class CreacionEventoGeneralAdministradorDTO
    {
        public CreacionDetalleEventoDTO DetalleEvento { get; set; }

        public List<IFormFile> Documentos { get; set; } = new();
    }
}
