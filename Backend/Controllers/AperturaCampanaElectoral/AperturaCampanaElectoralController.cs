

using Backend.DTOs.AperturaCampanaElectoral;
using Backend.DTOs.Utils;
using Backend.Repositorios.AperturaCampanaElectoral;
using Backend.Repositorios.Roles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.AperturaCampanaElectoral
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AperturaCampanaElectoralController : ControllerBase
    {
        private readonly IRepositorioAperturaCampanaElectoral _repositorioCampana;

        public AperturaCampanaElectoralController(IRepositorioAperturaCampanaElectoral repositorioCampana)
        {
            this._repositorioCampana = repositorioCampana;
        }

        [HttpGet]
        public async Task<ActionResult<List<AperturaCampanaElectoralDTO>>> get()
        {
            try
            {
                return await _repositorioCampana.get();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{codigo:int}")]
        public async Task<ActionResult<Entidades.AperturaCampanaElectoral>> getid(int codigo)
        {
            try
            {
                var dato = await _repositorioCampana.getid(codigo);
                if (dato.Value == null)
                {
                    Entidades.AperturaCampanaElectoral emp = new Entidades.AperturaCampanaElectoral();
                    return emp;

                }
                return dato;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("insertarapertura")]
        public async Task<ActionResult<EncabezadoDatos>> post(CreacionAperturaCampanaElectoralDTO creacion)
        {

            try
            {
                return await _repositorioCampana.post(creacion);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        [HttpPut("actualizarapertura/{codigo:int}")]
        public async Task<ActionResult<EncabezadoDatos>> put(int codigo, [FromBody] CreacionAperturaCampanaElectoralDTO edicion)
        {
            try
            {
                return await _repositorioCampana.put(codigo, edicion);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        [HttpDelete("{codigo:int}")]
        public async Task<ActionResult<EncabezadoDatos>> delete(int codigo)
        {
            try
            {
                return await _repositorioCampana.delete(codigo);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        [HttpGet("selectapertura/{codigo:int}")]
        public async Task<ActionResult<List<SelectFormulario>>> selectapertura()
        {
            try
            {
                return await _repositorioCampana.selectapertura();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
