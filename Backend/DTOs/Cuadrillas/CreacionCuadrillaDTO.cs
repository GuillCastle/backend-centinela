using Backend.DTOs.Usuario;
using Backend.Entidades;

namespace Backend.DTOs.Cuadrillas
{
    public class CreacionCuadrillaDTO
    {
        public CreacionEncabezadoCuadrillaDTO EncabezadoCuadrilla { get; set; }

        public List<CreacionDetalleCuadrillaDTO> DetalleSupervisores { get; set; }

        public List<CreacionDetalleCuadrillaDTO> DetalleCentinelas { get; set; }

        public List<UsuarioDTO>? DetallePersonas { get; set; }

    }
}
