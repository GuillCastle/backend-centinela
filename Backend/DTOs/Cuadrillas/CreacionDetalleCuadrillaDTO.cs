namespace Backend.DTOs.Cuadrillas
{
    public class CreacionDetalleCuadrillaDTO
    {
        public int Codigo {  get; set; }
        public int Cuadrilla { get; set; }

        public int Usuario { get; set; }

        public int Estado { get; set; }

        public int UsuarioRegistro { get; set; }

        public DateTime FechaRegistro { get; set; }

        public int TipoCuadrilla { get; set; }
    }
}
