using AutoMapper;
using Backend.DTOs.Rol;
using Backend.DTOs.Utils;
using Backend.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;


namespace Backend.Repositorios.Roles
{

    public class RepositorioRol : IRepositorioRol
    {
        private readonly ILogger<RepositorioRol> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RepositorioRol(ILogger<RepositorioRol> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ActionResult<List<RolDTO>>> get()
        {
            try
            {
                List<RolDTO> entity = await (from rol in context.Rols
                                                     select new
                                                     {
                                                         Codigo = rol.Codigo,
                                                         Descripcion = rol.Nombre,
                                                         Estado = rol.Estado == 1 ? "Activo" : "Inactivo"
                                                     }).Select(concepto => new RolDTO
                                                     {
                                                         Codigo = concepto.Codigo,
                                                         Nombre = concepto.Descripcion,
                                                         Estado = concepto.Estado
                                                     }).ToListAsync();

                return entity;
            }
            catch (Exception ex)
            {
                return new ObjectResult(JObject.Parse(ex.Message.ToString()));
            }
        }
        public async Task<ActionResult<RolEditarDTO>> getid(int codigo)
        {
            try
            {
                RolEditarDTO? entity = new RolEditarDTO();

                entity = await (from rol in context.Rols
                                where rol.Codigo == codigo
                                select new
                                {
                                    Codigo = rol.Codigo,
                                    Descripcion = rol.Nombre,
                                    Estado = rol.Estado
                                }).Select(concepto => new RolEditarDTO
                                {
                                    Codigo = concepto.Codigo,
                                    Nombre = concepto.Descripcion,
                                    Estado = concepto.Estado
                                }).FirstOrDefaultAsync();


                return entity;
            }
            catch (Exception ex)
            {
                return new ObjectResult(JObject.Parse(ex.Message.ToString()));
            }
        }

        public async Task<ActionResult<string>> postRol([FromBody] CreacionRolDTO creacionRol)
        {
            try
            {
                using var transaction = context.Database.BeginTransaction();

                var rol = mapper.Map<Rol>(creacionRol);
                if (rol == null)
                {
                    return "Rol se encuentra vacio";
                }
                context.Add(rol);
                await context.SaveChangesAsync();
                transaction.Commit();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public async Task<ActionResult<string>> put(int codigo, [FromBody] CreacionRolDTO creacionRol)
        {
            try
            {
                using var transaction = context.Database.BeginTransaction();
                var rol = await context.Rols.FirstOrDefaultAsync(x => x.Codigo == codigo);
                if (rol == null)
                {
                    return new ObjectResult("asdfasd");
                }

                rol = mapper.Map(creacionRol, rol);
                rol.Estado = creacionRol.Estado;

                await context.SaveChangesAsync();
                transaction.Commit();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public async Task<ActionResult<List<SelectFormulario>>> getcmbrol()
        {
            try
            {
                List<SelectFormulario> lista = await context.Rols
                                            .Where(rol => rol.Estado == 1) // Filtro por Estado == 1
                                            .Select(rol => new SelectFormulario // Proyección a ListaCmbRol
                                            {
                                                codigo = rol.Codigo,
                                                descripcion = rol.Nombre,
                                                // Mapear otros campos según tu definición
                                            })
                                            .ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {
                return new ObjectResult(JObject.Parse(ex.Message.ToString()));
            }
        }

        public async Task<ActionResult<List<RolPermisoDTO>>> getrolpermiso(int rol)
        {
            try
            {
                var query = from p in context.Permisos
                            join rp in context.RolPermisos
                            on new { Permiso = p.Codigo, Rol = rol } equals new { rp.Permiso, rp.Rol } into permisosRol
                            from permisoRol in permisosRol.DefaultIfEmpty()
                            select new RolPermisoDTO
                            {
                                Role = rol,
                                Permiso = p.Codigo,
                                Nombre = p.Nombre,
                                Busqueda = permisoRol != null ? permisoRol.Busqueda : 0,
                                Insertar = permisoRol != null ? permisoRol.Insertar : 0,
                                Reimpresion = permisoRol != null ? permisoRol.Reimpresion : 0,
                            };
                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return new ObjectResult(JObject.Parse(ex.Message.ToString()));
            }
        }

        public async Task<ActionResult<string>> inactivarpermisorol([FromBody] CreacionRolPermisoDTO rolpermisoDTO)
        {
            try
            {
                using var transaction = context.Database.BeginTransaction();

                var rol = mapper.Map<Entidades.RolPermiso>(rolpermisoDTO);

                if (rol == null)
                {
                    return "Rol se encuentra vacio";
                }

                Entidades.RolPermiso entity = await (from rolvalidacion in context.RolPermisos
                                           where rolvalidacion.Rol == rol.Rol && rolvalidacion.Permiso == rol.Permiso
                                           select rolvalidacion)
               .FirstOrDefaultAsync<Entidades.RolPermiso>();

                if (entity == null)
                {
                    context.Add(rol);
                    await context.SaveChangesAsync();
                    transaction.Commit();
                    return "1";
                }
                else
                {
                    entity.Insertar = rol.Insertar;
                    entity.Busqueda = rol.Busqueda;
                    entity.Reimpresion = rol.Reimpresion;
                    await context.SaveChangesAsync();
                    transaction.Commit();
                    return "1";
                }
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
    }
}
