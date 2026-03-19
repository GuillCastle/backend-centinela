using Backend.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Presentacion
{
    public class CreacionPresentacionDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo permite almacenar 250 caracteres")]
        public string Descripcion { get; set; }
        [Range(0, 1, ErrorMessage = "El rango para el {0} es de 0 a 1")]
        public int Estado { get; set; }
        public string Usuario { get; set; }
        public DateTime Fecha { get; set; }
    }
}
