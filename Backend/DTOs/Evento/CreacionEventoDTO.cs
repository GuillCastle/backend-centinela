namespace Backend.DTOs.Evento
{
    public class CreacionEventoDTO
    {
        public string Descripcion { get; set; } = null!;

        public int Estado { get; set; }

        public int Usuario { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
