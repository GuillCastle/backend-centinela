using Backend.DTOs.Apertura;
using Backend.DTOs.Reportes;
using Backend.Repositorios.Reportes.ReportesApertura;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Reportes.ReportesApertura
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ReportesAperturaController : ControllerBase
    {
        private readonly IRepositorioReportesApertura repositorioReportesApertura;

        public ReportesAperturaController(IRepositorioReportesApertura _repositorioReportesApertura)
        {
            this.repositorioReportesApertura = _repositorioReportesApertura;
        }

        [HttpGet("obteneractivosapertura/{apertura:int}/{tiporeporte:int}/{usuario:int}")]
        public async Task<ActionResult<ReportesGenerales>> obteneractivosapertura(int apertura, int tiporeporte, int usuario)
        {
            try
            {
                return await repositorioReportesApertura.obteneractivosapertura(apertura, tiporeporte, usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
