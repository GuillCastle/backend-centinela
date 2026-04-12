using Backend.Entidades;

namespace Backend.DTOs.Evento
{
    public class EventoGeneralDTO
    {
        public EventoIdDTO EncabezadoEvento { get; set; }

        public List<DetalleEventoDTO> DetalleEventoGenerado { get; set; }

        public List<DetalleEventoDocumentoDTO> DetalleDocumentoGenerado { get; set; }
    }
}
