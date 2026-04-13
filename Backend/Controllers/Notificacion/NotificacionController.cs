using Backend.DTOs.Notificacion;
using Backend.Repositorios.Evento;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers.Notificacion
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NotificacionController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public NotificacionController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("pendientes/{usuario}")]
        public async Task<ActionResult<List<NotificacionDTO>>> ObtenerPendientes(int usuario)
        {
            try
            {
                var lista = await context.Notificacions
                    .Where(x => x.Usuario == usuario && x.Estado == 1 && x.Leida == 0)
                    .OrderByDescending(x => x.FechaRegistro)
                    .Select(x => new NotificacionDTO
                    {
                        Codigo = x.Codigo,
                        Usuario = x.Usuario,
                        Titulo = x.Titulo,
                        Mensaje = x.Mensaje,
                        Tipo = x.Tipo,
                        ReferenciaId = x.Evento,
                        Leida = x.Leida,
                        FechaRegistro = x.FechaRegistro
                    })
                    .ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message });
            }
        }

        [HttpPut("marcarleida/{id}")]
        public async Task<IActionResult> MarcarLeida(long id)
        {
            try
            {
                var notificacion = await context.Notificacions
                    .FirstOrDefaultAsync(x => x.Codigo == id && x.Estado == 1);

                if (notificacion == null)
                {
                    return new NotFoundObjectResult(new { message = "No se encontró la notificación" });
                }

                notificacion.Leida = 1;
                await context.SaveChangesAsync();

                return Ok(new { message = "1" });
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message });
            }
        }
    }
}
