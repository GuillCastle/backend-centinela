using Backend.DTOs.Login;
using Backend.DTOs.Marca;
using Backend.DTOs.Rol;
using Backend.DTOs.Usuario;
using Backend.Repositorios.Login;
using Backend.Repositorios.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Login
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class LoginController : ControllerBase
    {
        private readonly IRepositorioLogin _repositorioLogin;
            
        public LoginController(IRepositorioLogin repositorioLogin)
        {
            this._repositorioLogin = repositorioLogin;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<RespuestaAutenticacion>> login([FromBody] CredencialesUsuario credenciales)
        {
            try
            {
                return await  _repositorioLogin.login(credenciales);
            }
            catch (Exception ex)
            {
                RespuestaAutenticacion respuesta = new RespuestaAutenticacion();
                respuesta.api_token = ex.Message.ToString(); 
                return respuesta;
            }

        }
        
    }
}
