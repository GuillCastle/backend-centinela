using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Login
{
    public class CredencialesUsuario
    {
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
