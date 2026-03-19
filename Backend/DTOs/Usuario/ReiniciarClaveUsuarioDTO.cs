namespace Backend.DTOs.Usuario
{
    public class ReiniciarClaveUsuarioDTO
    {
        public int Codigo { get; set; }

        public string NombreUsuario { get; set; } = null!;

        public string Nombre { get; set; } = null!;

        public string CorreoElectronico { get; set; } = null!;

        public string Telefono { get; set; } = null!;

        public string Clave { get; set; } = null!;

        public string Salt { get; set; } = null!;

        public int Estado { get; set; }

        public int Rol { get; set; }

        public string UsuarioRegistro { get; set; } = null!;

        public string FechaRegistro { get; set; } = null!;

        public string? Foto { get; set; }

        public int? Entidad { get; set; }

        public int? Sucursal { get; set; }
    }
}
