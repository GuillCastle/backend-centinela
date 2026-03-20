using System.ComponentModel.DataAnnotations;

namespace Backend.DTOs.Login
{
    public class RespuestaAutenticacion
    {
        public string verificador {  get; set; }
        public string api_token { get; set; }
        public string refreshToken { get; set; }
    }
}
