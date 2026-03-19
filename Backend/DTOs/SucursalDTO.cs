using Backend.Entidades;
using Backend.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs
{
    public class SucursalDTO
    {
        public int Codigo { get; set; }
        public int Empresa { get; set; } // Required reference navigation to principal
        public string NombreEmpresa { get; set; }
        public string CodigoSucursal { get; set; }
        public string Descripcion { get; set; }
        public string Abreviatura { get; set; }
        public string Encargado { get; set; }
        public string Telefono { get; set; }
        public string CorreoElectronico { get; set; }
        public string Estado { get; set; }
        public string? CorreoCopia { get; set; }
        public string? LLaveSucursal { get; set; }
        public string? LlaveFirma { get; set; }
    }
}
