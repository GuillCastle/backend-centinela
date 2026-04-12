using Backend.DTOs.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repositorios.TipoEvento
{
    public interface IRepositorioTipoEvento
    {
        Task<ActionResult<List<SelectFormulario>>> selecttipoevento();
    }
}
