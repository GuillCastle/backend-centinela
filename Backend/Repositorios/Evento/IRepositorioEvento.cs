using Backend.DTOs.Evento;
using Backend.DTOs.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repositorios.Evento
{
    public interface IRepositorioEvento
    {
        Task<ActionResult<EncabezadoDatos>> delete(int codigo);
        Task<IActionResult> descargararchivo(long id);
        Task<ActionResult<EncabezadoDatos>> insertarsolodetalleevento([FromBody] CreacionDetalleEventoDTO Creacion);
        Task<ActionResult<EncabezadoDatos>> insertarsolodetalleeventoadministrador([FromForm] CreacionEventoGeneralAdministradorDTO Creacion);
        Task<ActionResult<EventoGeneralDTO>> obtenereventodetalle(int evento);
        Task<ActionResult<List<EventoDTO>>> obtenereventofinalizados();
        Task<ActionResult<List<EventoDTO>>> obtenereventousuarioadministrador(int usuario);
        Task<ActionResult<List<EventoDTO>>> obtenereventousuariocentinela(int usuario);
        Task<ActionResult<List<EventoDTO>>> obtenereventousuariosupervisor(int usuario);
        Task<ActionResult<EncabezadoDatos>> post([FromForm] CreacionEventoGeneralDTO Creacion);
    }
}
