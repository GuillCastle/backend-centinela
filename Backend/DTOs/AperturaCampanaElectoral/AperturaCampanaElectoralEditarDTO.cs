namespace Backend.DTOs.AperturaCampanaElectoral
{
    public class AperturaCampanaElectoralEditarDTO
    {
        public int Codigo { get; set; }

        public string Descripcion { get; set; } = null!;

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public int Estado { get; set; }
    }
}
