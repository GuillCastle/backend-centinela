namespace Backend.DTOs.Apertura
{
    public class ListaAperturaCreditosGeneralesIdDTO
    {
        public long Codigo { get; set; }

        public int Persona { get; set; }

        public string Motivo { get; set; } = null!;

        public decimal Monto { get; set; }

        public int Entidad { get; set; }

        public int Sucursal { get; set; }

    }
}
