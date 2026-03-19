using Backend.DTOs.Clientes;
using Backend.DTOs.Shared;
using Backend.DTOs.Utils;
using Backend.Entidades;
using Backend.Repositorios.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Shared
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class SharedController : ControllerBase
    {
        private readonly IRepositorioShared repositorioShared;

        public SharedController(IRepositorioShared repositorioshared)
        {
            this.repositorioShared = repositorioshared;
        }

        [HttpGet("selectdepartamento/{pais:int}")]
        public async Task<ActionResult<List<SelectFormulario>>> obtenerDepartamento(int pais)
        {
            try
            {
                return await repositorioShared.obtenerDepartamento(pais);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("selectmunicipio/{departamento:int}")]
        public async Task<ActionResult<List<SelectFormulario>>> obtenerMunicipio(int departamento)
        {
            try
            {
                return await repositorioShared.obtenerMunicipio(departamento);
            }
            catch (Exception ex)
            {   
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("obtenerdatoscertificadornit/{nit}/{entidad:int}/{sucursal:int}/{tipo:int}")]
        public async Task<ActionResult<RespuestaNitDpiInfileDTO>> obtenerdatoscertificadornit(string nit, int entidad, int sucursal, int tipo)
        {
            try
            {
                return await repositorioShared.obtenerdatoscertificadornit(nit, entidad, sucursal, tipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("obtenerdatoscertificadordpi/{dpi}/{entidad:int}/{sucursal:int}/{tipo:int}")]
        public async Task<ActionResult<RespuestaNitDpiInfileDTO>> obtenerdatoscertificadordpi(string dpi, int entidad, int sucursal, int tipo)
        {
            try
            {
                return await repositorioShared.obtenerdatoscertificadordpi(dpi, entidad, sucursal, tipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("validaciondpiclienteproveedor/{dpi}/{tipo:int}")]
        public async Task<ActionResult<RespuestaNitDpiInfileDTO>> validacionDpiClienteProveedor(string dpi, int tipo)
        {
            try
            {
                return await repositorioShared.validacionDpiClienteProveedor(dpi, tipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("validacionnitclienteproveedor/{nit}/{tipo:int}")]
        public async Task<ActionResult<RespuestaNitDpiInfileDTO>> ValidacioNitClienteProveedor(string nit, int tipo)
        {
            try
            {
                return await repositorioShared.ValidacioNitClienteProveedor(nit, tipo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("selectcombustible")]
        public async Task<ActionResult<List<SelectFormulario>>> selectcombustible()
        {
            try
            {
                return await repositorioShared.selectcombustible();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpGet("obtenerdatoscombustible/{codigo:int}")]
        public async Task<ActionResult<Combustible>> obtenerdatoscombustible(int codigo)
        {
            try
            {
                return await repositorioShared.obtenerdatoscombustible(codigo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

    }
    
}
