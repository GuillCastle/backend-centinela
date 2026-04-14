using Backend.DTOs.Evento;
using Backend.DTOs.Mapas;
using Backend.Repositorios.Evento;
using Backend.Repositorios.Mapas;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Mapas
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MapasController : ControllerBase
    {
        private readonly IRepositorioMapas _repositorioMapas;
        public MapasController(IRepositorioMapas repositorioMapas)
        {
            this._repositorioMapas = repositorioMapas;
        }

        [HttpGet("obtenerMapaDepartamento")]
        public async Task<ActionResult<List<DepartamentoMapaDTO>>> obtenerMapaDepartamento()
        {
            try
            {
                return await _repositorioMapas.obtenerMapaDepartamento();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("obtenerMapaEventos/{codigo:int}")]
        public async Task<ActionResult<List<EventoMapaDTO>>> obtenerMapaEventos(int codigo)
        {
            try
            {
                return await _repositorioMapas.obtenerMapaEventos(codigo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
