using Backend.DTOs.Shared;
using Backend.DTOs.Utils;
using Backend.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repositorios.Shared
{
    public interface IRepositorioShared
    {
        Task<ActionResult<RespuestaNitDpiInfileDTO>> obtenerdatoscertificadordpi(string dpi, int entidad, int sucursal, int tipo);
        Task<ActionResult<RespuestaNitDpiInfileDTO>> obtenerdatoscertificadornit(string nit, int entidad, int sucursal, int tipo);
        Task<ActionResult<List<SelectFormulario>>> obtenerDepartamento(int pais);
        Task<ActionResult<List<SelectFormulario>>> obtenerMunicipio(int departamento);
        Task<ActionResult<List<SelectFormulario>>> selectcombustible();
        Task<ActionResult<Combustible>> obtenerdatoscombustible(int codigo);
        Task<ActionResult<RespuestaNitDpiInfileDTO>> validacionDpiClienteProveedor(string dpi, int tipo);
        Task<ActionResult<RespuestaNitDpiInfileDTO>> ValidacioNitClienteProveedor(string nit, int tipo);
    }
}
