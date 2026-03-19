namespace Backend.DTOs.Proveedores
{
    public class ProveedoresDTO
    {
        public int Codigo { get; set; }
        public string? Nit { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string CorreoElectronico { get; set; } = null!;
        public string Usuario { get; set; } = null!;
        public DateTime FechaRegistro { get; set; }
        public string Estado { get; set; } = null!;
        public string? Retencion { get; set; } = null!;
        public string? Conceptos { get; set; } = null!;
        public string? PequenoContribuyente { get; set; }
        public string? Banco { get; set; } = null!;
        public string? Cuenta { get; set; }
        public string RetenedorIva { get; set; } = null!;
        public string? Pais { get; set; } = null!;
        public string? Departamento { get; set; } = null!;
        public string? Municipio { get; set; } = null!;
        public string? TipoProveedor { get; set; } = null!;
        public string? DPI { get; set; }
       
        
        
       

    }
}
