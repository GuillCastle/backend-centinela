namespace Backend.DTOs.Apertura
{
    public class ListaAperturaCreditosGeneral
    {
        public long Codigo { get; set; }
        public string Persona { get; set; } = null!;

        public string Motivo { get; set; } = null!;

        public decimal Monto { get; set; }

        public string Entidad { get; set; } = null!;

        public string Sucursal { get; set; } = null!;

    }
}
