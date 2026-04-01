using AutoMapper;
using Backend.DTOs.AperturaCampanaElectoral;
using Backend.DTOs.Cuadrillas;
using Backend.DTOs.Usuario;
using Backend.DTOs.Utils;
using Backend.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Backend.Repositorios.Cuadrillas
{
    public class RepositorioCuadrillas : IRepositorioCuadrillas
    {
        private readonly ILogger<RepositorioCuadrillas> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RepositorioCuadrillas(ILogger<RepositorioCuadrillas> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ActionResult<List<EncabezadoCuadrillaDTO>>> get()
        {
            try
            {
                List<EncabezadoCuadrillaDTO> entity = await (from cuadrilla in context.EncabezadoCuadrillas
                                                                  select new
                                                                  {
                                                                      Codigo = cuadrilla.Codigo,
                                                                      Descripcion = cuadrilla.Descripcion,
                                                                      Estado = cuadrilla.Estado == 1 ? "Activo" : "Inactivo",
                                                                  }).Select(concepto => new EncabezadoCuadrillaDTO
                                                                  {
                                                                      Codigo = concepto.Codigo,
                                                                      Descripcion = concepto.Descripcion,
                                                                      Estado = concepto.Estado,
                                                                  }).ToListAsync();

                return entity;
            }
            catch (Exception ex)
            {
                return new ObjectResult(JObject.Parse(ex.Message.ToString()));
            }
        }

        public async Task<ActionResult<EncabezadoCuadrilla>> getid(int codigo)
        {
            try
            {
                var dato = await context.EncabezadoCuadrillas.FirstOrDefaultAsync(x => x.Codigo == codigo);

                if (dato == null)
                {
                    return new ObjectResult(new { message = "No se encontro el registro" });
                }

                return dato;
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        public async Task<ActionResult<List<UsuarioDTO>>> obtenerusuariosrol(string rol)
        {
            try
            {
                if (rol == "4")
                {
                    int r = int.Parse(rol);
                    List<UsuarioDTO> entity = await context.Usuarios
                    .Where(u =>
                        u.Rol == r &&
                        u.Estado == 1 &&
                        !context.DetalleUsuarioCuadrillas.Any(d =>
                            d.Usuario == u.Codigo &&
                            d.Estado == 1 // 👈 CLAVE
                        )
                    )
                    .Select(u => new UsuarioDTO
                    {
                        Codigo = u.Codigo,
                        NombreUsuario = u.Nombre,
                        Nombre = u.Nombre,
                        CorreoElectronico = u.CorreoElectronico,
                        Telefono = u.Telefono
                    })
                    .ToListAsync();

                    return entity;
                }
                else
                {
                    int r = int.Parse(rol);
                    List<UsuarioDTO> entity = await (from cuadrilla in context.Usuarios
                                                     where
                                                     cuadrilla.Rol == r && cuadrilla.Estado == 1
                                                     select new
                                                     {
                                                         Codigo = cuadrilla.Codigo,
                                                         NombreUsuario = cuadrilla.Nombre,
                                                         Nombre = cuadrilla.Nombre,
                                                         Correo = cuadrilla.CorreoElectronico,
                                                         Telefono = cuadrilla.Telefono
                                                     }).Select(concepto => new UsuarioDTO
                                                     {
                                                         Codigo = concepto.Codigo,
                                                         NombreUsuario = concepto.NombreUsuario,
                                                         Nombre = concepto.Nombre,
                                                         CorreoElectronico = concepto.Correo,
                                                         Telefono = concepto.Telefono
                                                     }).ToListAsync();

                    return entity;
                }
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        public async Task<ActionResult<EncabezadoDatos>> insertarcuadrilla([FromBody] CreacionCuadrillaDTO Creacion)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var Nueva = mapper.Map<EncabezadoCuadrilla>(Creacion.EncabezadoCuadrilla);
                    if (Nueva == null)
                    {
                        return new ObjectResult(new { message = "No cuenta con datos la cuadrilla" });
                    }
                    context.Add(Nueva);
                    await context.SaveChangesAsync();

                    foreach(var item in Creacion.DetalleSupervisores)
                    {
                        DetalleUsuarioCuadrilla detalleSupervisor = new DetalleUsuarioCuadrilla();

                        detalleSupervisor.Cuadrilla = int.Parse(Nueva.Codigo.ToString());
                        detalleSupervisor.Usuario = item.Usuario;
                        detalleSupervisor.Estado = item.Estado;
                        detalleSupervisor.UsuarioRegistro = item.UsuarioRegistro;
                        detalleSupervisor.FechaRegistro = DateTime.Now;
                        detalleSupervisor.TipoCuadrilla = item.TipoCuadrilla;
                        context.DetalleUsuarioCuadrillas.Add(detalleSupervisor);
                    }

                    foreach (var item in Creacion.DetalleCentinelas)
                    {
                        DetalleUsuarioCuadrilla detalleCentinela = new DetalleUsuarioCuadrilla();

                        detalleCentinela.Cuadrilla = int.Parse(Nueva.Codigo.ToString());
                        detalleCentinela.Usuario = item.Usuario;
                        detalleCentinela.Estado = item.Estado;
                        detalleCentinela.UsuarioRegistro = item.UsuarioRegistro;
                        detalleCentinela.FechaRegistro = DateTime.Now;
                        detalleCentinela.TipoCuadrilla = item.TipoCuadrilla;
                        context.DetalleUsuarioCuadrillas.Add(detalleCentinela);
                    }
                    await context.SaveChangesAsync();
                    transaction.Commit();
                    EncabezadoDatos datos = new EncabezadoDatos();
                    datos.id = int.Parse(Nueva.Codigo.ToString());
                    datos.mensaje = "1";
                    return datos;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    if (ex.InnerException?.Message.Length == 0 || ex.InnerException?.Message == null)
                    {
                        return new ObjectResult(new { message = ex.Message });

                    }
                    else
                    {
                        return new ObjectResult(new { message = ex.InnerException?.Message });
                    }
                }
            }
        }

        public async Task<ActionResult<CreacionCuadrillaDTO>> obtenercuadrillas(int codigo)
        {
            try
            {
                CreacionCuadrillaDTO datos = new CreacionCuadrillaDTO();

                List<CreacionDetalleCuadrillaDTO> listaSupervisores = await (from supervisores in context.DetalleUsuarioCuadrillas
                                                             where supervisores.Estado == 1 && 
                                                             supervisores.Cuadrilla == codigo &&
                                                             supervisores.TipoCuadrilla == 2
                                                             select new
                                                             {
                                                                 Codigo = supervisores.Codigo,
                                                                 Cuadrilla = supervisores.Cuadrilla,
                                                                 Usuario = supervisores.Usuario,
                                                                 Estado = supervisores.Estado,
                                                                 TipoCuadrilla = supervisores.TipoCuadrilla,
                                                             }).Select(concepto => new CreacionDetalleCuadrillaDTO
                                                             {
                                                                 Codigo = concepto.Codigo,
                                                                 Cuadrilla = concepto.Cuadrilla,
                                                                 Usuario = concepto.Usuario,
                                                                 Estado = concepto.Estado,
                                                                 TipoCuadrilla = concepto.TipoCuadrilla,
                                                             }).ToListAsync();

                List<CreacionDetalleCuadrillaDTO> listaCentinelas = await (from supervisores in context.DetalleUsuarioCuadrillas
                                                            where supervisores.Estado == 1 &&
                                                            supervisores.Cuadrilla == codigo &&
                                                            supervisores.TipoCuadrilla == 1
                                                            select new
                                                            {
                                                                Codigo = supervisores.Codigo,
                                                                Cuadrilla = supervisores.Cuadrilla,
                                                                Usuario = supervisores.Usuario,
                                                                Estado = supervisores.Estado,
                                                                TipoCuadrilla = supervisores.TipoCuadrilla,
                                                            }).Select(concepto => new CreacionDetalleCuadrillaDTO
                                                            {
                                                                Codigo = concepto.Codigo,
                                                                Cuadrilla = concepto.Cuadrilla,
                                                                Usuario = concepto.Usuario,
                                                                Estado = concepto.Estado,
                                                                TipoCuadrilla = concepto.TipoCuadrilla,
                                                            }).ToListAsync();


                List<UsuarioDTO> entity = await (
                    from usuario in context.Usuarios
                    where usuario.Rol == 4
                        //&& usuario.Estado == 1
                        && context.DetalleUsuarioCuadrillas.Any(d =>
                            d.Usuario == usuario.Codigo &&
                            d.Cuadrilla == codigo &&
                            d.Estado == 1 &&
                            d.TipoCuadrilla == 1)
                    select new UsuarioDTO
                    {
                        Codigo = usuario.Codigo,
                        NombreUsuario = usuario.Nombre,
                        Nombre = usuario.Nombre,
                        CorreoElectronico = usuario.CorreoElectronico,
                        Telefono = usuario.Telefono
                    }
                ).ToListAsync();

                datos.DetalleSupervisores = listaSupervisores;
                datos.DetalleCentinelas = listaCentinelas;
                datos.DetallePersonas = entity;

                return datos;
            }
            catch (Exception ex)
            {
                return new ObjectResult(JObject.Parse(ex.Message.ToString()));
            }
        }

        public async Task<ActionResult<EncabezadoDatos>> actualizarcuadrilla(int codigo, [FromBody] CreacionCuadrillaDTO Edicion)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var nuevo = await context.EncabezadoCuadrillas.FirstOrDefaultAsync(x => x.Codigo == codigo);
                    if (nuevo == null)
                    {
                        return new ObjectResult(new { message = "No se encontro el registro a actualizar" });
                    }
                    nuevo = mapper.Map(Edicion.EncabezadoCuadrilla, nuevo);

                    await context.SaveChangesAsync();

                    List<CreacionDetalleCuadrillaDTO> listaSupervisores = await (from supervisores in context.DetalleUsuarioCuadrillas
                                                                                 where supervisores.Estado == 1 &&
                                                                                 supervisores.Cuadrilla == codigo &&
                                                                                 supervisores.TipoCuadrilla == 2
                                                                                 select new
                                                                                 {
                                                                                     Codigo = supervisores.Codigo,
                                                                                     Cuadrilla = supervisores.Cuadrilla,
                                                                                     Usuario = supervisores.Usuario,
                                                                                     Estado = supervisores.Estado,
                                                                                     TipoCuadrilla = supervisores.TipoCuadrilla,
                                                                                 }).Select(concepto => new CreacionDetalleCuadrillaDTO
                                                                                 {
                                                                                     Codigo = concepto.Codigo,
                                                                                     Cuadrilla = concepto.Cuadrilla,
                                                                                     Usuario = concepto.Usuario,
                                                                                     Estado = concepto.Estado,
                                                                                     TipoCuadrilla = concepto.TipoCuadrilla,
                                                                                 }).ToListAsync();

                    List<CreacionDetalleCuadrillaDTO> listaCentinelas = await (from supervisores in context.DetalleUsuarioCuadrillas
                                                                               where supervisores.Estado == 1 &&
                                                                               supervisores.Cuadrilla == codigo &&
                                                                               supervisores.TipoCuadrilla == 1
                                                                               select new
                                                                               {
                                                                                   Codigo = supervisores.Codigo,
                                                                                   Cuadrilla = supervisores.Cuadrilla,
                                                                                   Usuario = supervisores.Usuario,
                                                                                   Estado = supervisores.Estado,
                                                                                   TipoCuadrilla = supervisores.TipoCuadrilla,
                                                                               }).Select(concepto => new CreacionDetalleCuadrillaDTO
                                                                               {
                                                                                   Codigo = concepto.Codigo,
                                                                                   Cuadrilla = concepto.Cuadrilla,
                                                                                   Usuario = concepto.Usuario,
                                                                                   Estado = concepto.Estado,
                                                                                   TipoCuadrilla = concepto.TipoCuadrilla,
                                                                               }).ToListAsync();

                    var supervisoresNuevos = Edicion.DetalleSupervisores ?? new List<CreacionDetalleCuadrillaDTO>();
                    var centinelasNuevos = Edicion.DetalleCentinelas ?? new List<CreacionDetalleCuadrillaDTO>();

                    var supervisoresInsertar = supervisoresNuevos
                        .Where(nuevo => !listaSupervisores
                            .Any(bd => bd.Usuario == nuevo.Usuario && bd.TipoCuadrilla == nuevo.TipoCuadrilla))
                        .ToList();

                    var supervisoresInactivar = listaSupervisores
                        .Where(bd => !supervisoresNuevos
                            .Any(nuevo => nuevo.Usuario == bd.Usuario && nuevo.TipoCuadrilla == bd.TipoCuadrilla))
                        .ToList();

                    var centinelasInsertar = centinelasNuevos
                        .Where(nuevo => !listaCentinelas.Any(bd => bd.Usuario == nuevo.Usuario))
                        .ToList();

                    var centinelasInactivar = listaCentinelas
                        .Where(bd => !centinelasNuevos.Any(nuevo => nuevo.Usuario == bd.Usuario))
                        .ToList();

                    foreach (var item in supervisoresInsertar)
                    {
                        context.DetalleUsuarioCuadrillas.Add(new DetalleUsuarioCuadrilla
                        {
                            Cuadrilla = codigo,
                            Usuario = item.Usuario,
                            Estado = 1,
                            UsuarioRegistro = item.UsuarioRegistro,
                            FechaRegistro = DateTime.Now,
                            TipoCuadrilla = item.TipoCuadrilla
                        });
                    }

                    foreach (var item in centinelasInsertar)
                    {
                        context.DetalleUsuarioCuadrillas.Add(new DetalleUsuarioCuadrilla
                        {
                            Cuadrilla = codigo,
                            Usuario = item.Usuario,
                            Estado = 1,
                            UsuarioRegistro = item.UsuarioRegistro,
                            FechaRegistro = DateTime.Now,
                            TipoCuadrilla = item.TipoCuadrilla
                        });
                    }

                    var idsSupervisoresInactivar = supervisoresInactivar.Select(x => x.Codigo).ToList();
                    var idsCentinelasInactivar = centinelasInactivar.Select(x => x.Codigo).ToList();

                    var registrosInactivar = await context.DetalleUsuarioCuadrillas
                        .Where(x => idsSupervisoresInactivar.Contains(x.Codigo) || idsCentinelasInactivar.Contains(x.Codigo))
                        .ToListAsync();

                    foreach (var item in registrosInactivar)
                    {
                        item.Estado = 0;
                    }

                    await context.SaveChangesAsync();

                    transaction.Commit();
                    EncabezadoDatos datos = new EncabezadoDatos();
                    datos.id = codigo;
                    datos.mensaje = "1";
                    return datos;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    if (ex.InnerException?.Message.Length == 0 || ex.InnerException?.Message == null)
                    {
                        return new ObjectResult(new { message = ex.Message });

                    }
                    else
                    {
                        return new ObjectResult(new { message = ex.InnerException?.Message });
                    }
                }
            }
        }

        public async Task<ActionResult<EncabezadoDatos>> delete(int codigo, int tipo)
        {
            try
            {
                if (tipo == 1)
                {
                    using var transaction = await context.Database.BeginTransactionAsync();

                    var apertura = await context.EncabezadoCuadrillas
                        .FirstOrDefaultAsync(x => x.Codigo == codigo);

                    if (apertura == null)
                    {
                        return new ObjectResult(new { message = "No se encontro el registro a desactivar" });
                    }

                    var detalles = await context.DetalleUsuarioCuadrillas
                        .Where(x => x.Estado == 1 && x.Cuadrilla == codigo)
                        .ToListAsync();

                    apertura.Estado = 0;

                    foreach (var item in detalles)
                    {
                        item.Estado = 0;
                    }

                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();

                }
                else if(tipo == 2)
                {
                    using var transaction = context.Database.BeginTransaction();
                    var Inactivar = await context.EncabezadoCuadrillas.FirstOrDefaultAsync(x => x.Codigo == codigo);
                    var apertura = await context.EncabezadoCuadrillas.FirstOrDefaultAsync(x => x.Codigo == codigo);
                    if (apertura == null)
                    {
                        return new ObjectResult(new { message = "No se encontro el registro a desactivar" });
                    }

                    apertura = mapper.Map(Inactivar, apertura);



                    List<CreacionDetalleCuadrillaDTO> lista = await (from supervisores in context.DetalleUsuarioCuadrillas
                                                                     where supervisores.Estado == 0 &&
                                                                     supervisores.Cuadrilla == codigo
                                                                     select new
                                                                     {
                                                                         Codigo = supervisores.Codigo,
                                                                         Cuadrilla = supervisores.Cuadrilla,
                                                                         Usuario = supervisores.Usuario,
                                                                         Estado = supervisores.Estado,
                                                                         TipoCuadrilla = supervisores.TipoCuadrilla,
                                                                     }).Select(concepto => new CreacionDetalleCuadrillaDTO
                                                                     {
                                                                         Codigo = concepto.Codigo,
                                                                         Cuadrilla = concepto.Cuadrilla,
                                                                         Usuario = concepto.Usuario,
                                                                         Estado = concepto.Estado,
                                                                         TipoCuadrilla = concepto.TipoCuadrilla,
                                                                     }).ToListAsync();

                    apertura.Estado = 1;
                    foreach (var item in lista)
                    {
                        item.Estado = 1;
                    }
                    await context.SaveChangesAsync();

                    transaction.Commit();
                    
                }

                EncabezadoDatos datos = new EncabezadoDatos();
                datos.id = codigo;
                datos.mensaje = "1";
                return datos;

            }
            catch (Exception ex)
            {
                if (ex.InnerException?.Message.Length == 0)
                {
                    return new ObjectResult(new { message = ex.Message });

                }
                else
                {
                    return new ObjectResult(new { message = ex.InnerException?.Message });
                }
            }
        }

    }
}
