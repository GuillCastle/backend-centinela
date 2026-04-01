namespace Backend.DTOs.Cuadrillas
{
    public class CreacionEncabezadoCuadrillaDTO
    {
        public string Descripcion { get; set; } = null!;

        public int Estado { get; set; }

        public int UsuarioRegistro { get; set; }

        public DateTime FechaRegistro { get; set; }
    }
}
