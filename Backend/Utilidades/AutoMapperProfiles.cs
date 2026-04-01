using AutoMapper;
using Backend.DTOs;
using Backend.DTOs.AperturaCampanaElectoral;
using Backend.DTOs.Cuadrillas;
using Backend.DTOs.Rol;

using Backend.DTOs.Usuario;

using Backend.Entidades;

namespace Backend.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            ///marcas
           
            CreateMap<Empresa, EmpresaDTO>().ReverseMap();
            CreateMap<CreacionEmpresaDTO, Empresa>();
            CreateMap<Sucursale, SucursalDTO>().ReverseMap();
            CreateMap<CreacionSucursalDTO, Sucursale>();
            CreateMap<Rol, RolDTO>().ReverseMap();
            CreateMap<CreacionRolDTO, Rol>();
            CreateMap<CreacionRolPermisoDTO, RolPermiso>().ReverseMap();

           

            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<CreacionUsuarioDTO, Usuario>();
            CreateMap<CreacionUsuarioSIDTO, Usuario>();

           

            CreateMap<CreacionUsuarioPermisoDTO, UsuarioPermiso>().ReverseMap();

            CreateMap<AperturaCampanaElectoral, AperturaCampanaElectoralDTO>().ReverseMap();
            CreateMap<CreacionAperturaCampanaElectoralDTO, AperturaCampanaElectoral>().ReverseMap();

            CreateMap<EncabezadoCuadrilla, CreacionEncabezadoCuadrillaDTO>().ReverseMap();
            CreateMap<DetalleUsuarioCuadrilla, CreacionDetalleCuadrillaDTO>().ReverseMap();


        }
    }
}
