namespace Backend.DTOs.Evento
{
    public class DetalleEventoDTO
    {
        public long Codigo { get; set; }

        public int Evento { get; set; }

        public string Descripcion { get; set; } = null!;

        public int Usuario { get; set; }

        public DateTime FechaRegistro { get; set; }

        public int TipoCuadrilla { get; set; }
    }
}
