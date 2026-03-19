using Backend.DTOs;
using Backend.DTOs.Utils;
using Backend.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repositorios.EmpresaSucursal
{
    public interface IRepositorioEmpresaSucursal
    {
        Task<ActionResult<string>> delete(int codigo);
        Task<ActionResult<List<EmpresaDTO>>> get();
        Task<ActionResult<EmpresaDTO>> getid(int codigo);
        Task<ActionResult<List<SucursalDTO>>> getSucursal();
        Task<ActionResult<SucursalDTO>> getSucursalid(int codigo);
        Task<ActionResult<string>> post([FromBody] CreacionEmpresaDTO creacionEmpresa);
        Task<ActionResult<string>> postSucursal([FromBody] CreacionSucursalDTO creacionSucursal);
        Task<ActionResult<string>> put(int codigo, [FromBody] CreacionEmpresaDTO empresaEdicion);
        Task<ActionResult<string>> putSucursal(int codigo, [FromBody] CreacionSucursalDTO sucursalEdicion);
        Task<ActionResult<List<SelectFormulario>>> selectentidad();
        Task<ActionResult<List<SelectFormulario>>> selectsucursal(int empresa);
    }
}
