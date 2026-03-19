namespace Backend.DTOs.Rol
{
    public class RolEditarDTO
    {
        public int Codigo { get; set; }

        public string Nombre { get; set; } = null!;

        public int Estado { get; set; }
    }
}
