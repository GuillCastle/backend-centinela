using Backend.DTOs.Utils;
using Backend.Repositorios.Shared;
using Backend.Repositorios.TipoEvento;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.TipoEvento
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TipoEventoController : ControllerBase
    {
        private readonly IRepositorioTipoEvento _repositorioTipoEvento;

        public TipoEventoController(IRepositorioTipoEvento repositorioTipoEvento)
        {
            this._repositorioTipoEvento = repositorioTipoEvento;
        }

        [HttpGet("selecttipoevento")]
        public async Task<ActionResult<List<SelectFormulario>>> selecttipoevento()
        {
            try
            {
                return await _repositorioTipoEvento.selecttipoevento();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
