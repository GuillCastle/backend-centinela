using Backend.DTOs.AperturaCampanaElectoral;
using Backend.DTOs.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repositorios.AperturaCampanaElectoral
{
    public interface IRepositorioAperturaCampanaElectoral
    {
        Task<ActionResult<EncabezadoDatos>> delete(int codigo);
        Task<ActionResult<List<AperturaCampanaElectoralDTO>>> get();
        Task<ActionResult<Entidades.AperturaCampanaElectoral>> getid(int codigo);
        Task<ActionResult<EncabezadoDatos>> post([FromBody] CreacionAperturaCampanaElectoralDTO Creacion);
        Task<ActionResult<EncabezadoDatos>> put(int codigo, [FromBody] CreacionAperturaCampanaElectoralDTO Edicion);
        Task<ActionResult<List<SelectFormulario>>> selectapertura();
    }
}
