namespace Backend.DTOs.Proveedores
{
    public class ProveedoresidDTO
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; } = null!;
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Correo { get; set; } = null!;
        public int Contacto { get; set; }
        public int Estado { get; set; }
        public DateTime FechaRegistro { get; set; }
        public int UsuarioRegistro { get; set; }
        public int TipoProveedor { get; set; }
        public int? Retencion { get; set; }
        public int? Conceptos { get; set; }
        public int? PequeñoContribuyente { get; set; }
        public int? Banco { get; set; }
        public string? Cuenta { get; set; } = null!;
        public int? RetenedorIva { get; set; }
        public int? Pais { get; set; }
        public int? Departamento { get; set; }
        public int? Municipio { get; set; }
        public string? Nit { get; set; }
        public string Dpi { get; set; }
    }
}
