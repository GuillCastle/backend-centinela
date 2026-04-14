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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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

        [HttpGet("obtenereventodetalle/{evento:int}")]
        public async Task<ActionResult<EventoGeneralDTO>> obtenereventodetalle(int evento)
        {
            try
            {
                return await _repositorioEvento.obtenereventodetalle(evento);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("insertarsolodetalleevento")]
        public async Task<ActionResult<EncabezadoDatos>> insertarsolodetalleevento([FromBody] CreacionDetalleEventoDTO creacion)
        {
            try
            {
                return await _repositorioEvento.insertarsolodetalleevento(creacion);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        [HttpPost("insertarsolodetalleeventoadministrador")]
        public async Task<ActionResult<EncabezadoDatos>> insertarsolodetalleeventoadministrador([FromForm] CreacionEventoGeneralAdministradorDTO creacion)
        {

            try
            {
                return await _repositorioEvento.insertarsolodetalleeventoadministrador(creacion);
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
                return await _repositorioEvento.delete(codigo);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        [HttpGet("obtenereventofinalizados")]
        public async Task<ActionResult<List<EventoDTO>>> obtenereventofinalizados()
        {
            try
            {
                return await _repositorioEvento.obtenereventofinalizados();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
