namespace Backend.DTOs.Usuario
{
    public class UsuarioDTO
    {
        public int Codigo { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string CorreoElectronico { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        //public string Clave { get; set; } = null!;

        //public string Salt { get; set; } = null!;

        public string Estado { get; set; }

        public string Rol { get; set; }

        //public string UsuarioRegistro { get; set; } = null!;

        //public string FechaRegistro { get; set; } = null!;

        public string? Foto { get; set; }
        public string? Entidad { get; set; }

        public string? Sucursal { get; set; }
    }
}
