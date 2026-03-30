
using Backend.Repositorios.Cuadrillas;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Cuadrillas
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuadrillasController : ControllerBase
    {
        private readonly IRepositorioCuadrillas _repositoriocuadrillas;
        public CuadrillasController(IRepositorioCuadrillas repositorioCuadrillas)
        {
            this._repositoriocuadrillas = repositorioCuadrillas;
        }

        [HttpGet]
        public async Task<ActionResult<List<AperturaCampanaElectoralDTO>>> get()
        {
            try
            {
                return await _repositoriocuadrillas.get();
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
                var dato = await _repositoriocuadrillas.getid(codigo);
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

    }
}
