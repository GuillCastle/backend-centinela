namespace Backend.DTOs.Notificacion
{
    public class NotificacionDTO
    {
        public long Codigo { get; set; }
        public int Usuario { get; set; }
        public string Titulo { get; set; } = null!;
        public string Mensaje { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public int? ReferenciaId { get; set; }
        public int Leida { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
