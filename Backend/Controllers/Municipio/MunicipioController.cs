using Backend.DTOs.Municipio;
using Backend.Repositorios.Municipio;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Municipio
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MunicipioController : ControllerBase
    {
        private readonly IRepositorioMunicipio repositoriomunicipio;

        public MunicipioController(IRepositorioMunicipio repositorioMunicipio)
        {
            this.repositoriomunicipio = repositorioMunicipio;
        }

        [HttpGet]
        public async Task<ActionResult<List<MunicipioDTO>>> get()
        {
            try
            {
                return await repositoriomunicipio.get();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /*[HttpGet("{codigo:int}")]
        public async Task<ActionResult<MunicipioIdDTO>> getid(int codigo)
        {
            try
            {
                return await repositoriomunicipio.getid(codigo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<string>> post([FromBody] CreacionMunicipioDTO municipiocreacion)
        {

            try
            {
                return await repositoriomunicipio.post(municipiocreacion);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [HttpPut("{codigo:int}")]
        public async Task<ActionResult<string>> put(int codigo, [FromBody] CreacionMunicipioDTO municipioedicion)
        {
            try
            {
                return await repositoriomunicipio.put(codigo, municipioedicion);
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
                return await repositoriomunicipio.delete(codigo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
    }
}
