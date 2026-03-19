using Backend.DTOs.Rol;
using Backend.DTOs.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repositorios.Roles
{
    public interface IRepositorioRol
    {
        Task<ActionResult<List<RolDTO>>> get();
        Task<ActionResult<List<SelectFormulario>>> getcmbrol();
        Task<ActionResult<RolEditarDTO>> getid(int codigo);
        Task<ActionResult<List<RolPermisoDTO>>> getrolpermiso(int rol);
        Task<ActionResult<string>> inactivarpermisorol([FromBody] CreacionRolPermisoDTO rolpermisoDTO);
        Task<ActionResult<string>> postRol([FromBody] CreacionRolDTO creacionRol);
        Task<ActionResult<string>> put(int codigo, [FromBody] CreacionRolDTO creacionRol);
    }
}
