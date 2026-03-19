using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Municipio
{
    public class CreacionMunicipioDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo permite almacenar 50 caracteres")]
        public string Descripcion { get; set; } = null!;

        [Range(0, 1, ErrorMessage = "El rango para el {0} es de 0 a 1")]
        public int Departamento { get; set; }
        public string CodigoPostal { get; set; } = null!;
    }
}
