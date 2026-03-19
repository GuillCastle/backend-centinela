using Backend.Entidades;
using Backend.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs
{
    public class CreacionSucursalDTO
    {

        public int EmpresaCodigo { get; set; }

        public string CodigoSucursal { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        public string Abreviatura { get; set; } = null!;

        public string Encargado { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string CorreoElectronico { get; set; } = null!;

        public int Estado { get; set; }

        public DateTime FechaRegistro { get; set; }

        public string Usuario { get; set; } = null!;

        public string? CorreoSucursal { get; set; }

        public string? CodigoPostal { get; set; }

        public string? Pais { get; set; }

        public int? Departamento { get; set; }

        public int? Municipio { get; set; }

        public string? Direccion { get; set; }

        public string? UsuarioCertificado { get; set; }

        public string? CorreoCopia { get; set; }

        public string? LlaveSucursal { get; set; }

        public string? LlaveFirma { get; set; }
    }
}
