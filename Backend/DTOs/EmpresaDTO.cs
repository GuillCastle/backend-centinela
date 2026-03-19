using Backend.Entidades;
using Backend.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs
{
    public class EmpresaDTO
    {
        public string CodigoEmpresa { get; set; }
        public string Nombre { get; set; }
        public string Nit { get; set; }
        public string Representante { get; set; }
        public string Regimen { get; set; }
        public string AfiliacionIva { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono { get; set; }
        public string Estado { get; set; }
        public int Nivel { get; set; }
        public string Alias { get; set; }
        public ICollection<Sucursale> Sucursales { get; set; }

    }
}
