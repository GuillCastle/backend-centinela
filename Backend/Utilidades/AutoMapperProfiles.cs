using AutoMapper;
using Backend.DTOs;
using Backend.DTOs.Apertura;
using Backend.DTOs.Banco;
using Backend.DTOs.Clientes;
using Backend.DTOs.Compras;
using Backend.DTOs.CuentaBancariaProveedor;
using Backend.DTOs.Cuentas;
using Backend.DTOs.DetalleCompraActivo;
using Backend.DTOs.DetalleCompraCombustible;
using Backend.DTOs.DetalleCompraProducto;
using Backend.DTOs.DetalleCompraServicio;
using Backend.DTOs.DetalleVentaActivo;
using Backend.DTOs.DetalleVentaProducto;
using Backend.DTOs.DetalleVentaServicio;
using Backend.DTOs.Marca;
using Backend.DTOs.Presentacion;
using Backend.DTOs.Producto;
using Backend.DTOs.Proveedores;
using Backend.DTOs.Rol;
using Backend.DTOs.UnidadMedida;
using Backend.DTOs.Usuario;
using Backend.DTOs.Ventas.Venta;
using Backend.Entidades;

namespace Backend.Utilidades
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            ///marcas
            CreateMap<Marca, MarcasDTO>().ReverseMap();
            CreateMap<CreacionMarcaDTO, Marca>();
            CreateMap<Empresa, EmpresaDTO>().ReverseMap();
            CreateMap<CreacionEmpresaDTO, Empresa>();
            CreateMap<Sucursale, SucursalDTO>().ReverseMap();
            CreateMap<CreacionSucursalDTO, Sucursale>();
            CreateMap<Rol, RolDTO>().ReverseMap();
            CreateMap<CreacionRolDTO, Rol>();
            CreateMap<CreacionRolPermisoDTO, RolPermiso>().ReverseMap();

            CreateMap<UnidadMedidum, UnidadMedidaDTO>().ReverseMap();
            CreateMap<CreacionUnidadMedidaDTO, UnidadMedidum>();

            CreateMap<Presentacion, PresentacionDTO>().ReverseMap();
            CreateMap<CreacionPresentacionDTO, Presentacion>();

            CreateMap<Usuario, UsuarioDTO>().ReverseMap();
            CreateMap<CreacionUsuarioDTO, Usuario>();
            CreateMap<CreacionUsuarioSIDTO, Usuario>();

            CreateMap<Producto, ProductoDTO>().ReverseMap();
            CreateMap<CreacionProductoDTO, Producto>();

            CreateMap<CreacionUsuarioPermisoDTO, UsuarioPermiso>().ReverseMap();

            CreateMap<AperturaInventario, AperturaDTO>().ReverseMap();

            CreateMap<Cuenta, CuentasDTO>().ReverseMap();
            CreateMap<CreacionCuentaDTO, Cuenta>();

            CreateMap <Proveedore, ProveedoresDTO>().ReverseMap();
            CreateMap<CreacionProveedoresDTO, Proveedore>();

            CreateMap<ProveedorCuentaBancarium, CuentasBanProveedoreDTO>().ReverseMap();
            CreateMap<CreacionCuentasBanProveedoresDTO, ProveedorCuentaBancarium>();

            CreateMap<Cliente, ClientesDTO>().ReverseMap();
            CreateMap<CreacionClientesDTO, Cliente>();

            CreateMap<Banco, BancoDTO>().ReverseMap();
            CreateMap<CreacionBancoDTO, Banco>();

            CreateMap<CreacionEncabezadoAperturaDTO, AperturaInventario>();
            CreateMap<DetalleAperturaDTO,  DetalleApertura>();

            CreateMap<DetalleAperturaActivo, DetalleAperturaActivoDTO>().ReverseMap();

            CreateMap<CreacionAperturaCreditoGeneralesDTO, AperturaCreditoCompra>()
            .ForMember(dest => dest.Proveedor, opt => opt.MapFrom(src => src.Persona))
            .ForMember(dest => dest.Codigo, opt => opt.Ignore()) // lo genera la BD normalmente
            .ForMember(dest => dest.ProveedorNavigation, opt => opt.Ignore());

            CreateMap<CreacionAperturaCreditoGeneralesDTO, AperturaCreditoVentum>()
            .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Persona))
            .ForMember(dest => dest.Codigo, opt => opt.Ignore()) // lo genera la BD normalmente
            .ForMember(dest => dest.ClienteNavigation, opt => opt.Ignore());

            //compras
            CreateMap<Compra, creacionCompraDTO>().ReverseMap();
            CreateMap<creacionCompraDTO, Compra>().ReverseMap();

            CreateMap<DetalleCompraProducto, CreacionDetalleCompraProductoDTO>().ReverseMap();

            CreateMap<DetalleCompraActivo, CreacionDetalleCompraActivoDTO>().ReverseMap();
            CreateMap<DetalleCompraServicio, CreacionDetalleCompraServicioDTO>().ReverseMap();
            CreateMap<DetalleCompraCombustible, CreacionDetalleCompraCombustibleDTO>().ReverseMap();

            //ventas
            CreateMap<Ventum, creacionVentaDTO>().ReverseMap();
            CreateMap<creacionVentaDTO, Ventum>().ReverseMap();

            CreateMap<DetalleVentaProducto, CreacionDetalleVentaProductoDTO>().ReverseMap();
            CreateMap<DetalleVentaServicio, CreacionDetalleVentaServicioDTO>().ReverseMap();
            CreateMap<DetalleVentaActivo, CreacionDetalleVentaActivoDTO>().ReverseMap();

        }
    }
}
