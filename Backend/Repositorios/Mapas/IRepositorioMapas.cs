using Backend.DTOs.Mapas;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repositorios.Mapas
{
    public interface IRepositorioMapas
    {
        Task<ActionResult<List<DepartamentoMapaDTO>>> obtenerMapaDepartamento();
        Task<ActionResult<List<EventoMapaDTO>>> obtenerMapaEventos(int codigo);
    }
}
