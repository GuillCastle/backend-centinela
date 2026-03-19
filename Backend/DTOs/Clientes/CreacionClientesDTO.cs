using System.ComponentModel.DataAnnotations;
namespace Backend.DTOs.Clientes
{
    public class CreacionClientesDTO
    {
        public string? Nit { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo permite almacenar 1000 caracteres")]
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string? CorreoElectronico { get; set; } = null!;
        public int Usuario { get; set; }
        public DateTime FechaRegistro { get; set; }

        [Range(0, 1, ErrorMessage = "El rango para el {0} es de 0 a 1")]
        public int Estado { get; set; }
        public int Pais { get; set; }
        public int Departamento { get; set; }
        public int Municipio { get; set; }
        public string? Dpi { get; set; }
    }
}
