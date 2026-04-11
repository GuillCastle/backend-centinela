namespace Backend.DTOs.Evento
{
    public class EventoDTO
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; } = null!;

        public string Estado { get; set; }

        public string Usuario { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
