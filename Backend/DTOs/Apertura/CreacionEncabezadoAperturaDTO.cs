namespace Backend.DTOs.Apertura
{
    public class CreacionEncabezadoAperturaDTO
    {
        public string Descripcion { get; set; } = null!;

        public DateTime Fecha { get; set; }

        public string Usuario { get; set; } = null!;

        public string? Documento { get; set; } = null!;

        public int Entidad { get; set; }

        public int Sucursal { get; set; }

        public int Estado { get; set; }

        public int? Anio { get; set; }
    }
}
