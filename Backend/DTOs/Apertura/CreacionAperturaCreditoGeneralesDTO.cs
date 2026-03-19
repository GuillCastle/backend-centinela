namespace Backend.DTOs.Apertura
{
    public class CreacionAperturaCreditoGeneralesDTO
    {
        public int Persona { get; set; }

        public DateTime Fecha { get; set; }

        public string Motivo { get; set; } = null!;

        public decimal Monto { get; set; }

        public int Usuario { get; set; }

        public int Entidad { get; set; }

        public int Sucursal { get; set; }

        public int Estado { get; set; }

        public int Apertura { get; set; }
    }
}
