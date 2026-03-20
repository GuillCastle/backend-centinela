
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

    }
    
}
