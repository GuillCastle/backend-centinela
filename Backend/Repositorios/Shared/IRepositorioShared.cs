using Backend.DTOs.Shared;
using Backend.DTOs.Utils;
using Backend.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repositorios.Shared
{
    public interface IRepositorioShared
    {
        Task<ActionResult<List<SelectFormulario>>> obtenerDepartamento(int pais);
        Task<ActionResult<List<SelectFormulario>>> obtenerMunicipio(int departamento);

    }
}
