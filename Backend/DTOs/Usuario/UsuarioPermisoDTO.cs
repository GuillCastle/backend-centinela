namespace Backend.DTOs.Usuario
{
    public class UsuarioPermisoDTO
    {
        public int Usuario { get; set; }

        public int Permiso { get; set; }

        public int Busqueda { get; set; }

        public int Insertar { get; set; }

        public int Reimpresion { get; set; }

        public string Nombre { get; set; }
    }
}
