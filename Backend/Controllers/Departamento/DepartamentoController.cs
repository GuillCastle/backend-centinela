using Backend.DTOs.Departamento;
using Backend.DTOs.Utils;
using Backend.Repositorios.Departamento;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Departamento
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DepartamentoController : ControllerBase
    {
        private readonly IRepositorioDepartamento repositoriodepartamento;

        public DepartamentoController(IRepositorioDepartamento repositoriodepartamento)
        {
            this.repositoriodepartamento = repositoriodepartamento;
        }

        [HttpGet]
        public async Task<ActionResult<List<DepartamentoDTO>>> get()
        {
            try
            {
                return await repositoriodepartamento.get();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /*[HttpGet("{codigo:int}")]
        public async Task<ActionResult<DepartamentoIdDTO>> getid(int codigo)
        {
            try
            {
                return await repositoriodepartamento.getid(codigo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<string>> post([FromBody] CreaciondepartamentoDTO departamentocreacion)
        {

            try
            {
                return await repositoriodepartamento.post(departamentocreacion);
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        [HttpPut("{codigo:int}")]
        public async Task<ActionResult<string>> put(int codigo, [FromBody] CreaciondepartamentoDTO departamentoedicion)
        {
            try
            {
                return await repositoriodepartamento.put(codigo, departamentoedicion);
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
                return await repositoriodepartamento.delete(codigo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("selectDepartamento")]
        public async Task<ActionResult<List<SelectFormulario>>> obtenerDepartamento()
        {
            try
            {
                return await repositoriodepartamento.obtenerMunicipio();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
    }
}
