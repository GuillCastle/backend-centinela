using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Departamento
{
    public class CreacionDepartamentoDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo permite almacenar 50 caracteres")]

        public string Descripcion { get; set; } = null!;

        public int Pais { get; set; }
    }
}
