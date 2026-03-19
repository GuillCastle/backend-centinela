using Backend.DTOs;
using Backend.DTOs.Utils;
using Backend.Entidades;
using Backend.Repositorios.EmpresaSucursal;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.EmpresaSucursal
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmpresaSucursalController : ControllerBase
    {
        private readonly IRepositorioEmpresaSucursal repositorioEmpresa;

        public EmpresaSucursalController(IRepositorioEmpresaSucursal repositorioEmpresaSucursal)
        {
            this.repositorioEmpresa = repositorioEmpresaSucursal;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmpresaDTO>>> get()
        {
            try
            {
                return await repositorioEmpresa.get();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{codigo:int}")]
        public async Task<ActionResult<EmpresaDTO>> getid(int codigo)
        {
            try
            {
                var empresa = await repositorioEmpresa.getid(codigo);
                if(empresa.Value == null)
                {
                    EmpresaDTO emp = new EmpresaDTO();
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
        public async Task<ActionResult<string>> post([FromBody] CreacionEmpresaDTO empresaCreacion)
        {

            try
            {
                return await repositorioEmpresa.post(empresaCreacion);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [HttpPut("{codigo:int}")]
        public async Task<ActionResult<string>> put(int codigo, [FromBody] CreacionEmpresaDTO empresaEdicion)
        {
            try
            {
                return await repositorioEmpresa.put(codigo, empresaEdicion);
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
                return await repositorioEmpresa.delete(codigo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("sucursal")]
        public async Task<ActionResult<List<SucursalDTO>>> getSucursal()
        {
            try
            {
                return await repositorioEmpresa.getSucursal();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("sucursal/{codigo:int}")]
        public async Task<ActionResult<SucursalDTO>> getSucursalid(int codigo)
        {
            try
            {
                var empresa = await repositorioEmpresa.getSucursalid(codigo);
                if (empresa.Value == null)
                {
                    SucursalDTO emp = new SucursalDTO();
                    return emp;

                }
                return empresa;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("sucursal")]
        public async Task<ActionResult<string>> postSucursal([FromBody] CreacionSucursalDTO sucursalCreacion)
        {

            try
            {
                return await repositorioEmpresa.postSucursal(sucursalCreacion);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [HttpPut("sucursal/{codigo:int}")]
        public async Task<ActionResult<string>> putSucursal(int codigo, [FromBody] CreacionSucursalDTO sucursalEdicion)
        {
            try
            {
                return await repositorioEmpresa.putSucursal(codigo, sucursalEdicion);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [HttpGet("selectentidad")]
        public async Task<ActionResult<List<SelectFormulario>>> selectentidad()
        {
            try
            {
                return await repositorioEmpresa.selectentidad();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("selectsucursal/{empresa:int}")]
        public async Task<ActionResult<List<SelectFormulario>>> selectsucursal(int empresa)
        {
            try
            {
                return await repositorioEmpresa.selectsucursal(empresa);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
