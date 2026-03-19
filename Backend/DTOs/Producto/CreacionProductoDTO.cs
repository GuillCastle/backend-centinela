using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Producto
{
    public class CreacionProductoDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo permite almacenar 50 caracteres")]
        public string CodBusqueda { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(maximumLength: 250, ErrorMessage = "El campo {0} solo permite almacenar 1000 caracteres")]
        public string Descripcion { get; set; }
        public int Marca { get; set; }
        public int UnidadMedida { get; set; }
        public string Usuario { get; set; }
        public DateTime FechaRegistro { get; set; }

        [Range(0, 1, ErrorMessage = "El rango para el {0} es de 0 a 1")]
        public int Estado { get; set; }
        public int AgenteEconomico { get; set; } 
        public string? CodBarras { get; set; } 
        public string? DescripcionSAT { get; set; }
        
    }
}
