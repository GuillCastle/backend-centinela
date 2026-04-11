namespace Backend.DTOs.Evento
{
    public class CreacionDetalleEventoDTO
    {
        public int Evento { get; set; }

        public string Descripcion { get; set; } = null!;

        public int Usuario { get; set; }

        public DateTime FechaRegistro { get; set; }

        public int TipoCuadrilla { get; set; }
    }
}
