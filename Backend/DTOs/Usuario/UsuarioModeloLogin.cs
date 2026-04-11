using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Backend.DTOs.Usuario
{
    public class UsuarioModeloLogin
    {
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string fullname { get; set; } = null!;
        public string occupation { get; set; } = null!;
        public string companyName { get; set; } = null!;
        public string phone { get; set; } = null!;
        public int entidad { get; set; }
        public int sucursal { get; set; }
        public string ruta { get; set; } = null!;
        public int rol { get; set; }

    }
}
