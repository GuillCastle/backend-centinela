using Backend.DTOs.Rol;
using Backend.DTOs.Utils;
using Backend.Repositorios.Roles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Rol
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RolController : ControllerBase
    {
        private readonly IRepositorioRol repositorioRol;

        public RolController(IRepositorioRol repositorioRol)
        {
            this.repositorioRol = repositorioRol;
        }

        [HttpGet]
        public async Task<ActionResult<List<RolDTO>>> get()
        {
            try
            {
                return await repositorioRol.get();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{codigo:int}")]
        public async Task<ActionResult<RolEditarDTO>> getid(int codigo)
        {
            try
            {
                var empresa = await repositorioRol.getid(codigo);
                if (empresa.Value == null)
                {
                    RolEditarDTO emp = new RolEditarDTO();
                    return emp;

                }
                return empresa;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<string>> post(CreacionRolDTO rolCreacion)
        {

            try
            {
                return await repositorioRol.postRol(rolCreacion);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [HttpPut("{codigo:int}")]
        public async Task<ActionResult<string>> put(int codigo, [FromBody] CreacionRolDTO empresaEdicion)
        {
            try
            {
                return await repositorioRol.put(codigo, empresaEdicion);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [HttpGet("cmbrol")]
        public async Task<ActionResult<List<SelectFormulario>>> getComboRol()
        {
            try
            {
                return await repositorioRol.getcmbrol();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("rolpermiso/{rol:int}")]
        public async Task<ActionResult<List<RolPermisoDTO>>> getrolpermiso(int rol)
        {
            try
            {
                return await repositorioRol.getrolpermiso(rol);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("inactivarpermisorol")]
        public async Task<ActionResult<string>> inactivarpermisorol([FromBody] CreacionRolPermisoDTO rolpermiso)
        {

            try
            {
                return await repositorioRol.inactivarpermisorol(rolpermiso);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

    }
}
