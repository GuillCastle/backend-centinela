using NetTopologySuite.Geometries;

namespace Backend.DTOs.Evento
{
    public class EventoIdDTO
    {
        public int Codigo { get; set; }

        public string Descripcion { get; set; } = null!;

        public int Estado { get; set; }

        public int Usuario { get; set; }

        public DateTime FechaRegistro { get; set; }

        public int AperturaCampanaElectoral { get; set; }

        public int? Departamento { get; set; }

        public int? Municipio { get; set; }

        public Geometry? PuntoGeografico { get; set; }

        public int? TipoEvento { get; set; }
    }
}
