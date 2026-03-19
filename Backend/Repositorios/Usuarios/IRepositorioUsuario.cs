using Backend.DTOs.Rol;
using Backend.DTOs.Usuario;
using Backend.DTOs.Utils;
using Backend.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repositorios.Usuarios
{
    public interface IRepositorioUsuario
    {
        Task<ActionResult<string>> actualizarusuario(int codigo, [FromForm] CreacionUsuarioDTO usuariopermisoDTO);
        Task<ActionResult<string>> actualizarusuariosi(int codigo, [FromForm] CreacionUsuarioSIDTO usuariopermisoDTO);
        Task<ActionResult<string>> delete(int codigo);
        Task<ActionResult<List<UsuarioDTO>>> get();
        Task<ActionResult<ReiniciarClaveUsuarioDTO>> getid(int codigo);
        Task<ActionResult<string>> inactivarpermisousaurio([FromBody] CreacionUsuarioPermisoDTO usuariopermisoDTO);
        Task<ActionResult<List<SelectFormulario>>> obtenercombousuario();
        Task<ActionResult<List<UsuarioPermisoDTO>>> obtenerpermisosusuario(int codigo);
        Task<ActionResult<List<RolPermisoDTO>>> obtenerrolpermiso(string usuario);
        Task<ActionResult<string>> post([FromForm] CreacionUsuarioDTO usuariocreacion);
        Task<ActionResult<string>> reiniciarclave(int codigo, [FromBody] ReiniciarClaveUsuarioDTO reiniciarclave);
        Task<ActionResult<UsuarioModeloLogin>> verificar_usuario(string usuario);
    }
}
