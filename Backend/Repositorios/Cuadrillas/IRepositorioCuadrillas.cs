using Backend.DTOs.Cuadrillas;
using Backend.DTOs.Usuario;
using Backend.DTOs.Utils;
using Backend.Entidades;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repositorios.Cuadrillas
{
    public interface IRepositorioCuadrillas
    {
        Task<ActionResult<EncabezadoDatos>> actualizarcuadrilla(int codigo, [FromBody] CreacionCuadrillaDTO Edicion);
        Task<ActionResult<EncabezadoDatos>> delete(int codigo, int tipo);
        Task<ActionResult<List<EncabezadoCuadrillaDTO>>> get();
        Task<ActionResult<EncabezadoCuadrilla>> getid(int codigo);
        Task<ActionResult<EncabezadoDatos>> insertarcuadrilla([FromBody] CreacionCuadrillaDTO Creacion);
        Task<ActionResult<CreacionCuadrillaDTO>> obtenercuadrillas(int codigo);
        Task<ActionResult<List<UsuarioDTO>>> obtenerusuariosrol(string rol);
    }
}
