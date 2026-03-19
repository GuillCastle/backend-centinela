using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Usuario
{
    public class CreacionUsuarioDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo permite almacenar 250 caracteres")]
        public string NombreUsuario { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo permite almacenar 250 caracteres")]
        public string Nombre { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string CorreoElectronico { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Telefono { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Clave { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Salt { get; set; } = null!;
        [Range(0, 1, ErrorMessage = "El rango para el {0} es de 0 a 1")]
        public int Estado { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public int Rol { get; set; }

        public string UsuarioRegistro { get; set; } = null!;

        public string FechaRegistro { get; set; } = null!;
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public IFormFile Foto { get; set; }
        public int Entidad { get; set; } 
        public int Sucursal { get; set; }
    }
}
