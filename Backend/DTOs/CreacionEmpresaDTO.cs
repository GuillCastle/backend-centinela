using Backend.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs
{
    public class CreacionEmpresaDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo permite almacenar 250 caracteres")]
        [PrimeraLetraMayuscula]
        public string CodigoEmpresa { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo permite almacenar 250 caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }
        [ValidacionesNit]
        public string Nit { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo permite almacenar 250 caracteres")]
        [PrimeraLetraMayuscula]
        public string Representante { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Regimen { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string AfiliacionIva { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string CorreoElectronico { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Usuario { get; set; }
        public string Alias { get; set; }
    }
}
