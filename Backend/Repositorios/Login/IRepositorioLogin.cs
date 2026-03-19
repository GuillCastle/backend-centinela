using Backend.DTOs.Login;
using Backend.DTOs.Rol;
using Backend.DTOs.Usuario;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repositorios.Login
{
    public interface IRepositorioLogin
    {
        Task<ActionResult<RespuestaAutenticacion>> login(CredencialesUsuario credenciales);
    }
}
