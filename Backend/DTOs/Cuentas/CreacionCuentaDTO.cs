
namespace Backend.DTOs.Cuentas
{
    public class CreacionCuentaDTO
    {
        public int? PadreCodigo { get; set; } = null!;
        public string CodigoNivel { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int Usuario { get; set; }
        public int Estado { get; set; }
        public int? NivelCuenta { get; set; } = null!;

    }
}
