using Backend.DTOs.Municipio;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repositorios.Municipio
{
    public interface IRepositorioMunicipio
    {
        Task<ActionResult<List<MunicipioDTO>>> get();
    }
}
