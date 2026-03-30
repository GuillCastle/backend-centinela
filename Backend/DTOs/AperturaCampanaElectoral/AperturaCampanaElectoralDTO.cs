namespace Backend.DTOs.AperturaCampanaElectoral
{
    public class AperturaCampanaElectoralDTO
    {
        public int Codigo { get; set; }

        public string Descripcion { get; set; } = null!;

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public string Estado { get; set; } = null!;

    }
}
