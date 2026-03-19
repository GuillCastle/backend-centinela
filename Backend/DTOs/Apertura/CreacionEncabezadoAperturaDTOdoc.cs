namespace Backend.DTOs.Apertura
{
    public class CreacionEncabezadoAperturaDTOdoc
    {
        public string Usuario { get; set; } = null!;
        public int Entidad { get; set; }

        public int Sucursal { get; set; }
        public IFormFile Documento { get; set; }
    }
}
