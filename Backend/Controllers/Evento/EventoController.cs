using Backend.DTOs.Cuadrillas;
using Backend.DTOs.Evento;
using Backend.DTOs.Usuario;
using Backend.DTOs.Utils;
using Backend.Repositorios.Evento;
using Backend.Repositorios.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Evento
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EventoController : ControllerBase
    {
        private readonly IRepositorioEvento _repositorioEvento;
        public EventoController(IRepositorioEvento repositorioEvento)
        {
            this._repositorioEvento = repositorioEvento;
        }

        [HttpPost]
        public async Task<ActionResult<EncabezadoDatos>> post([FromForm] CreacionEventoGeneralDTO creacion)
        {

            try
            {
                return await _repositorioEvento.post(creacion);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        [HttpGet("obtenereventousuariocentinela/{usuario:int}")]
        public async Task<ActionResult<List<EventoDTO>>> obtenereventousuariocentinela(int usuario)
        {
            try
            {
                return await _repositorioEvento.obtenereventousuariocentinela(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("descargararchivo/{id}")]
        public async Task<IActionResult> descargararchivo(long id)
        {
            try
            {
                return await _repositorioEvento.descargararchivo(id);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message });
            }
        }

        [HttpGet("obtenereventousuariosupervisor/{usuario:int}")]
        public async Task<ActionResult<List<EventoDTO>>> obtenereventousuariosupervisor(int usuario)
        {
            try
            {
                return await _repositorioEvento.obtenereventousuariosupervisor(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("obtenereventousuarioadministrador/{usuario:int}")]
        public async Task<ActionResult<List<EventoDTO>>> obtenereventousuarioadministrador(int usuario)
        {
            try
            {
                return await _repositorioEvento.obtenereventousuarioadministrador(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
