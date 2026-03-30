namespace Backend.DTOs.AperturaCampanaElectoral
{
    public class CreacionAperturaCampanaElectoralDTO
    {
        public string Descripcion { get; set; } = null!;

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public int Estado { get; set; }

        public int Usuario { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
