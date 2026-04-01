
using Backend.DTOs.AperturaCampanaElectoral;
using Backend.DTOs.Cuadrillas;
using Backend.DTOs.Rol;
using Backend.DTOs.Usuario;
using Backend.DTOs.Utils;
using Backend.Entidades;
using Backend.Repositorios.Cuadrillas;
using Backend.Repositorios.Usuarios;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.Cuadrillas
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CuadrillasController : ControllerBase
    {
        private readonly IRepositorioCuadrillas _repositoriocuadrillas;
        public CuadrillasController(IRepositorioCuadrillas repositorioCuadrillas)
        {
            this._repositoriocuadrillas = repositorioCuadrillas;
        }

        [HttpGet]
        public async Task<ActionResult<List<EncabezadoCuadrillaDTO>>> get()
        {
            try
            {
                return await _repositoriocuadrillas.get();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{codigo:int}")]
        public async Task<ActionResult<EncabezadoCuadrilla>> getid(int codigo)
        {
            try
            {
                var dato = await _repositoriocuadrillas.getid(codigo);
                if (dato.Value == null)
                {
                    Entidades.EncabezadoCuadrilla emp = new Entidades.EncabezadoCuadrilla();
                    return emp;

                }
                return dato;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("obtenerusuariosrol/{rol}")]
        public async Task<ActionResult<List<UsuarioDTO>>> obtenerusuariosrol(string rol)
        {
            try
            {
                return await _repositoriocuadrillas.obtenerusuariosrol(rol);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("insertarcuadrilla")]
        public async Task<ActionResult<EncabezadoDatos>> post(CreacionCuadrillaDTO creacion)
        {

            try
            {
                return await _repositoriocuadrillas.insertarcuadrilla(creacion);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        [HttpGet("obtenercuadrillas/{codigo:int}")]
        public async Task<ActionResult<CreacionCuadrillaDTO>> obtenercuadrillas(int codigo)
        {
            try
            {
                return await _repositoriocuadrillas.obtenercuadrillas(codigo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("actualizarcuadrilla/{codigo:int}")]
        public async Task<ActionResult<EncabezadoDatos>> actualizarcuadrilla(int codigo, CreacionCuadrillaDTO actualizacion)
        {

            try
            {
                return await _repositoriocuadrillas.actualizarcuadrilla(codigo, actualizacion);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        [HttpDelete("{codigo:int}/{tipo:int}")]
        public async Task<ActionResult<EncabezadoDatos>> delete(int codigo, int tipo)
        {
            try
            {
                return await _repositoriocuadrillas.delete(codigo, tipo);
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

    }
}
