using Backend.DTOs;
using Backend.DTOs.Rol;
using Backend.DTOs.Usuario;
using Backend.DTOs.Utils;
using Backend.Entidades;
using Backend.Repositorios.Roles;
using Backend.Repositorios.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Usuarios
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UsuariosController : ControllerBase
    {

        private readonly IRepositorioUsuario repositorioUsuario;
        public UsuariosController(IRepositorioUsuario repositoriousuario)
        {
            this.repositorioUsuario = repositoriousuario;
        }

        [HttpGet]
        public async Task<ActionResult<List<UsuarioDTO>>> get()
        {
            try
            {
                return await repositorioUsuario.get();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{codigo:int}")]
        public async Task<ActionResult<ReiniciarClaveUsuarioDTO>> getid(int codigo)
        {
            try
            {
                return await repositorioUsuario.getid(codigo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<string>> post([FromForm]CreacionUsuarioDTO usuarioCreacion)
        {

            try
            {
                return await repositorioUsuario.post(usuarioCreacion);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [HttpDelete("{codigo:int}")]
        public async Task<ActionResult<string>> delete(int codigo)
        {
            try
            {
                return await repositorioUsuario.delete(codigo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("reiniciarclave/{codigo:int}")]
        public async Task<ActionResult<string>> reiniciarclave(int codigo, [FromBody] ReiniciarClaveUsuarioDTO usuarioedicion)
        {
            try
            {
                return await repositorioUsuario.reiniciarclave(codigo, usuarioedicion);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [HttpGet("obtenerpermisosusuario/{codigo:int}")]
        public async Task<ActionResult<List<UsuarioPermisoDTO>>> obtenerpermisosusuario(int codigo)
        {
            try
            {
                return await repositorioUsuario.obtenerpermisosusuario(codigo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("obtenercombousuario")]
        public async Task<ActionResult<List<SelectFormulario>>> obtenercombousuario()
        {
            try
            {
                return await repositorioUsuario.obtenercombousuario();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("inactivarpermisousaurio")]
        public async Task<ActionResult<string>> inactivarpermisousaurio([FromBody] CreacionUsuarioPermisoDTO usuariopermisoDTO)
        {

            try
            {
                return await repositorioUsuario.inactivarpermisousaurio(usuariopermisoDTO);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [HttpPut("actualizarusuario/{codigo:int}")]
        public async Task<ActionResult<string>> actualizarusuario(int codigo, [FromForm] CreacionUsuarioDTO usuarioedicion)
        {
            try
            {
                return await repositorioUsuario.actualizarusuario(codigo, usuarioedicion);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [HttpPut("actualizarusuariosinimagen/{codigo:int}")]
        public async Task<ActionResult<string>> actualizarusuariosinimagen(int codigo, [FromForm] CreacionUsuarioSIDTO usuarioedicion)
        {
            try
            {
                return await repositorioUsuario.actualizarusuariosi(codigo, usuarioedicion);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [HttpGet("verificar_usuario/{usuario}")]
        public async Task<ActionResult<UsuarioModeloLogin>> verificar_usuario(string usuario)
        {
            try
            {
                return await repositorioUsuario.verificar_usuario(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("obtenerrolpermiso/{usuario}")]
        public async Task<ActionResult<List<RolPermisoDTO>>> obtenerrolpermiso(string usuario)
        {
            try
            {
                return await repositorioUsuario.obtenerrolpermiso(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("refresh")]
        public async Task<ActionResult<RefreshTokenResponseDTO>> refresh([FromBody] RefreshTokenRequestDTO refreshToken)
        {

            try
            {
                return await repositorioUsuario.refresh(refreshToken);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}
