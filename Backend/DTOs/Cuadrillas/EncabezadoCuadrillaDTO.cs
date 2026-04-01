namespace Backend.DTOs.Cuadrillas
{
    public class EncabezadoCuadrillaDTO
    {
        public int Codigo { get; set; }

        public string Descripcion { get; set; } = null!;

        public string Estado { get; set; } = null!;
    }
}
