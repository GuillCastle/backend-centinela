namespace Backend.DTOs.Evento
{
    public class DetalleEventoDocumentoDTO
    {
        public long Codigo { get; set; }

        public long DetalleEvento { get; set; }

        public string Url { get; set; } = null!;

        public int Estado { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
