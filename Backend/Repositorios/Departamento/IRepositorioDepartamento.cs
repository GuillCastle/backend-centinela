using Backend.DTOs.Departamento;
using Backend.DTOs.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repositorios.Departamento
{
    public interface IRepositorioDepartamento
    {
        Task<ActionResult<List<DepartamentoDTO>>> get();
        Task<ActionResult<DepartamentoIdDTO>> getid(int codigo);
        Task<ActionResult<List<SelectFormulario>>> obtenerDepartamento();
    }
}
