using Backend.DTOs.Reportes;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repositorios.Reportes.ReportesApertura
{
    public interface IRepositorioReportesApertura
    {
        Task<ActionResult<ReportesGenerales>> obteneractivosapertura(int apertura, int tiporeporte, int usuario);
    }
}
