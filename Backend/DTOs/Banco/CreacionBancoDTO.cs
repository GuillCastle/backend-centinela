using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Banco
{
    public class CreacionBancoDTO
    {
        
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo permite almacenar 1000 caracteres")]

        public string Descripcion { get; set; } = null!;

        public int Usuario { get; set; }

        public DateTime FechaRegistro { get; set; }

        [Range(0, 1, ErrorMessage = "El rango para el {0} es de 0 a 1")]
        public int Estado { get; set; }
    }
}
