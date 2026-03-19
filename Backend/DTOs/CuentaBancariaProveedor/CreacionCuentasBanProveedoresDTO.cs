using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.CuentaBancariaProveedor
{
    public class CreacionCuentasBanProveedoresDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Proveedor { get; set; }
        public int Banco { get; set; }
        public string Cuenta { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Principal { get; set; }
        public int Usuario { get; set; }
        public DateTime Fecha { get; set; }
        [Range(0, 1, ErrorMessage = "El rango para el {0} es de 0 a 1")]
        public int Estado { get; set; }

    }
}
