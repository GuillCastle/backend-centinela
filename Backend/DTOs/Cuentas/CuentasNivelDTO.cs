namespace Backend.DTOs.Cuentas
{
    public class CuentasNivelDTO
    {
        public int Codigo { get; set; }
        public string CodigoNivel { get; set; }
        public string Descripcion { get; set; }
        public int ?Nivel { get; set; }
        public int ?Padre { get; set; }
        public string DescripcionPadre { get; set; }
        public string Estado { get; set; }
    }
}
