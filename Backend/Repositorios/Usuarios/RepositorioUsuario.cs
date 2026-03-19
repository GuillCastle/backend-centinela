using AutoMapper;
using Backend.DTOs;
using Backend.DTOs.Producto;
using Backend.DTOs.Rol;
using Backend.DTOs.Usuario;
using Backend.DTOs.Utils;
using Backend.Entidades;
using Backend.Repositorios.Roles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Repositorios.Usuarios
{
    public class RepositorioUsuario :IRepositorioUsuario
    {
        private readonly ILogger<RepositorioUsuario> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RepositorioUsuario(ILogger<RepositorioUsuario> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ActionResult<List<UsuarioDTO>>> get()
        {
            try
            {
                List<UsuarioDTO> entity = await (from usuario in context.Usuarios 
                                                 join rol in context.Rols on usuario.Rol equals rol.Codigo
                                                 join empresa in context.Empresas on usuario.Entidad equals empresa.Codigo
                                                 join sucursal in context.Sucursales on usuario.Sucursal equals sucursal.Codigo
                                                 select new
                                                 {
                                                     Codigo = usuario.Codigo,
                                                     NombreUsuario = usuario.NombreUsuario,
                                                     Nombre = usuario.Nombre,
                                                     CorreoElectronico = usuario.CorreoElectronico,
                                                     Telefono = usuario.Telefono,
                                                     Estado = usuario.Estado == 1 ? "Activo" : "Inactivo",
                                                     Rol = rol.Nombre,
                                                     Foto = usuario.Foto,
                                                     Entidad = empresa.Nombre,
                                                     Sucursal = sucursal.Descripcion

                                                 }).Select(concepto => new UsuarioDTO
                                                 {
                                                     Codigo = concepto.Codigo,
                                                     NombreUsuario=concepto.NombreUsuario,
                                                     Nombre = concepto.Nombre,
                                                     CorreoElectronico = concepto.CorreoElectronico,
                                                     Telefono=concepto.Telefono,
                                                     Estado = concepto.Estado,
                                                     Rol = concepto.Rol,
                                                     Foto = concepto.Foto,
                                                     Entidad = concepto.Entidad,
                                                     Sucursal=concepto.Sucursal,
                                                 }).ToListAsync();

                return entity;
            }
            catch (Exception ex)
            {
                return new ObjectResult(JObject.Parse(ex.Message.ToString()));
            }
        }

        public async Task<ActionResult<ReiniciarClaveUsuarioDTO>> getid(int codigo)
        {
            try
            {
                ReiniciarClaveUsuarioDTO? usuarioid = new ReiniciarClaveUsuarioDTO();

                usuarioid = await (from usuario in context.Usuarios
                                    where usuario.Codigo == codigo
                                    select new
                                    {
                                        Codigo = usuario.Codigo,
                                        NombreUsuario = usuario.NombreUsuario,
                                        Nombre = usuario.Nombre,
                                        CorreoElectronico = usuario.CorreoElectronico,
                                        Telefono = usuario.Telefono,
                                        Clave = usuario.Clave,
                                        Salt = usuario.Salt,
                                        Estado = usuario.Estado,
                                        Rol = usuario.Rol,
                                        UsuarioRegistro = usuario.UsuarioRegistro,
                                        FechaRegistro = usuario.FechaRegistro,
                                        Foto = usuario.Foto,
                                        Entidad = usuario.Entidad,
                                        Sucursal = usuario.Sucursal
                                    }).Select(concepto => new ReiniciarClaveUsuarioDTO
                                    {
                                        Codigo = concepto.Codigo,
                                        NombreUsuario = concepto.NombreUsuario,
                                        Nombre = concepto.Nombre,
                                        CorreoElectronico = concepto.CorreoElectronico,
                                        Telefono = concepto.Telefono,
                                        Clave = concepto.Clave,
                                        Salt = concepto.Salt,
                                        Estado = concepto.Estado,
                                        Rol = concepto.Rol,
                                        UsuarioRegistro = concepto.UsuarioRegistro,
                                        FechaRegistro = concepto.FechaRegistro,
                                        Foto = concepto.Foto,
                                        Entidad = concepto.Entidad,
                                        Sucursal = concepto.Sucursal
                                    }).FirstOrDefaultAsync();

                if (usuarioid == null)
                {
                    return new ObjectResult(new { message = "No se encontro el registro" });
                }

                return usuarioid;
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        public async Task<ActionResult<string>> post([FromForm] CreacionUsuarioDTO usuariocreacion)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    ReiniciarClaveUsuarioDTO? usuarioid = new ReiniciarClaveUsuarioDTO();

                    usuarioid = await (from usuariov in context.Usuarios
                                       where usuariov.NombreUsuario == usuariocreacion.NombreUsuario
                                       select new
                                       {
                                           Codigo = usuariov.Codigo,
                                           NombreUsuario = usuariov.NombreUsuario,
                                           Nombre = usuariov.Nombre,
                                           CorreoElectronico = usuariov.CorreoElectronico,
                                           Telefono = usuariov.Telefono,
                                           Clave = usuariov.Clave,
                                           Salt = usuariov.Salt,
                                           Estado = usuariov.Estado,
                                           Rol = usuariov.Rol,
                                           UsuarioRegistro = usuariov.UsuarioRegistro,
                                           FechaRegistro = usuariov.FechaRegistro,
                                           Foto = usuariov.Foto,
                                           Entidad = usuariov.Entidad,
                                           Sucursal = usuariov.Sucursal
                                       }).Select(concepto => new ReiniciarClaveUsuarioDTO
                                       {
                                           Codigo = concepto.Codigo,
                                           NombreUsuario = concepto.NombreUsuario,
                                           Nombre = concepto.Nombre,
                                           CorreoElectronico = concepto.CorreoElectronico,
                                           Telefono = concepto.Telefono,
                                           Clave = concepto.Clave,
                                           Salt = concepto.Salt,
                                           Estado = concepto.Estado,
                                           Rol = concepto.Rol,
                                           UsuarioRegistro = concepto.UsuarioRegistro,
                                           FechaRegistro = concepto.FechaRegistro,
                                           Foto = concepto.Foto,
                                           Entidad = concepto.Entidad,
                                           Sucursal = concepto.Sucursal
                                       }).FirstOrDefaultAsync();

                    if (usuarioid != null)
                    {
                        return new ObjectResult(new { message = "Ya se ha creado un usuario con este nombre de usuario" });
                    }

                    var carpetaDestino = Path.Combine("E:", "imagenesCastillo");
                    if (!Directory.Exists(carpetaDestino))
                    {
                        Directory.CreateDirectory(carpetaDestino);
                    }
                    var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(usuariocreacion.Foto.FileName);
                    var rutaCompleta = Path.Combine(carpetaDestino, nombreArchivo);
                    using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                    {
                        await usuariocreacion.Foto.CopyToAsync(stream);
                    }
                    var rutaRelativa = Path.Combine("D:", "imagenesCastillo");

                    RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                    byte[] buff = new byte[6 - 1];
                    rng.GetBytes(buff);
                    string salt = Convert.ToBase64String(buff);

                    string saltAndPwd = string.Concat(usuariocreacion.Clave, salt);
                    string hashedPwd = "";
                    using (SHA1 sha1 = SHA1.Create())
                    {
                        byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(saltAndPwd));
                        hashedPwd = Convert.ToBase64String(hashBytes);
                    }


                    Usuario usuario = new Usuario();
                    usuario.NombreUsuario = usuariocreacion.NombreUsuario;
                    usuario.Nombre = usuariocreacion.Nombre;
                    usuario.CorreoElectronico = usuariocreacion.CorreoElectronico;
                    usuario.Telefono = usuariocreacion.Telefono;
                    usuario.Clave = hashedPwd;
                    usuario.Salt = salt;
                    usuario.Estado = usuariocreacion.Estado;
                    usuario.Rol = usuariocreacion.Rol;
                    usuario.UsuarioRegistro = usuariocreacion.UsuarioRegistro;
                    usuario.FechaRegistro = usuariocreacion.FechaRegistro;
                    usuario.Foto = rutaCompleta;
                    usuario.Entidad = usuariocreacion.Entidad;
                    usuario.Sucursal = usuariocreacion.Sucursal;

                    context.Add(usuario);
                    await context.SaveChangesAsync();
                    int nuevoUsuarioId = usuario.Codigo;
                    List<UsuarioPermiso> listaroles = await (from rol in context.Rols
                                                             join rolp in context.RolPermisos on rol.Codigo equals rolp.Rol
                                                             where rol.Estado == 1 && rol.Codigo == usuariocreacion.Rol
                                                             select new UsuarioPermiso
                                                             {
                                                                 Usuario = nuevoUsuarioId,
                                                                 Permiso = rolp.Permiso,
                                                                 Busqueda = rolp.Busqueda,
                                                                 Insertar = rolp.Insertar,
                                                                 Reimpresion = rolp.Reimpresion,
                                                                 // Entity Framework automáticamente maneja las relaciones de navegación
                                                             }).ToListAsync();


                    context.UsuarioPermisos.AddRange(listaroles);




                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return "1";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ObjectResult(new { message = ex.Message.ToString() });
                }
            }
        }

        public async Task<ActionResult<string>> delete(int codigo)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var usuarioInactivar = await context.Usuarios.FirstOrDefaultAsync(x => x.Codigo == codigo);
                    var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Codigo == codigo);
                    if (usuario == null)
                    {
                        return new ObjectResult(new { message = "No se encontro el registro a desactivar" });
                    }

                    usuario = mapper.Map(usuarioInactivar, usuario);

                    usuario.Estado = 0;


                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return "1";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ObjectResult(new { message = ex.Message.ToString() });
                }
            }
        }

        public async Task<ActionResult<string>> reiniciarclave(int codigo, [FromBody] ReiniciarClaveUsuarioDTO reiniciarclave)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var Usuarios = await context.Usuarios.FirstOrDefaultAsync(x => x.Codigo == codigo);
                    if (Usuarios == null)
                    {
                        return new ObjectResult(new { message = "No se encontro el registro para actualizar" });
                    }

                    //1q2w3e4r5t
                    Usuarios.Clave = "bzKg/f6TkPw4DilaUChgBiFmN+0=";
                    Usuarios.Salt = "8OiMxbc=";
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return "1";
                }
                catch (Exception ex)
                {
                    return new ObjectResult(new { message = ex.Message.ToString() });
                }
            }
        }

        public async Task<ActionResult<List<UsuarioPermisoDTO>>> obtenerpermisosusuario(int codigo)
        {
            try
            {
                var query = from p in context.Permisos
                            join rp in context.UsuarioPermisos
                            on new { Permiso = p.Codigo, Usuario = codigo } equals new { rp.Permiso, rp.Usuario } into permisosUsuario
                            from permisoUsuario in permisosUsuario.DefaultIfEmpty()
                            select new UsuarioPermisoDTO
                            {
                                Usuario = codigo,
                                Permiso = p.Codigo,
                                Nombre = p.Nombre,
                                Busqueda = permisoUsuario != null ? permisoUsuario.Busqueda : 0,
                                Insertar = permisoUsuario != null ? permisoUsuario.Insertar : 0,
                                Reimpresion = permisoUsuario != null ? permisoUsuario.Reimpresion : 0,
                            };

                return await query.ToListAsync();
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        public async Task<ActionResult<List<SelectFormulario>>> obtenercombousuario()
        {
            try
            {
                List<SelectFormulario> lista = await context.Usuarios
                                            .Select(usuario => new SelectFormulario // Proyección a ListaCmbRol
                                            {
                                                codigo = usuario.Codigo,
                                                descripcion = usuario.Nombre,
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

        public async Task<ActionResult<string>> inactivarpermisousaurio([FromBody] CreacionUsuarioPermisoDTO usuariopermisoDTO)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var usuario = mapper.Map<Entidades.UsuarioPermiso>(usuariopermisoDTO);

                    if (usuario == null)
                    {
                        return new ObjectResult(new { message = "Usuario se encuentra vacio" });
                    }

                    Entidades.UsuarioPermiso entity = await (from usuariovalidacion in context.UsuarioPermisos
                                                             where usuariovalidacion.Usuario == usuario.Usuario && usuariovalidacion.Permiso == usuario.Permiso
                                                             select usuariovalidacion)
                   .FirstOrDefaultAsync<Entidades.UsuarioPermiso>();

                    if (entity == null)
                    {
                        context.Add(usuario);
                        await context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return "1";
                    }
                    else
                    {
                        entity.Insertar = usuario.Insertar;
                        entity.Busqueda = usuario.Busqueda;
                        entity.Reimpresion = usuario.Reimpresion;
                        await context.SaveChangesAsync();
                        await transaction.CommitAsync();
                        return "1";
                    }
                }
                catch (Exception ex)
                {
                    return new ObjectResult(new { message = ex.Message.ToString() });
                }
            }
        }

        public async Task<ActionResult<string>> actualizarusuario(int codigo, [FromForm] CreacionUsuarioDTO usuarioDTO)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Codigo == codigo);
                    if (usuario == null)
                    {
                        return new ObjectResult(new { message = "No se encontro el registro para actualizar" });
                    }
                    var carpetaDestino = Path.Combine("E:", "imagenesCastillo");
                    if (!Directory.Exists(carpetaDestino))
                    {
                        Directory.CreateDirectory(carpetaDestino);
                    }
                    var carpetaDestinoEliminar = Path.Combine("D:", "imagenesCastillo");
                    if (!string.IsNullOrEmpty(usuario.Foto)) 
                    {
                        var rutaImagenAnterior = Path.Combine(carpetaDestinoEliminar, usuario.Foto);
                        if (System.IO.File.Exists(rutaImagenAnterior))
                        {
                            System.IO.File.Delete(rutaImagenAnterior); 
                        }
                    }

                    var nombreArchivo = Guid.NewGuid().ToString() + Path.GetExtension(usuarioDTO.Foto.FileName);
                    var rutaCompleta = Path.Combine(carpetaDestino, nombreArchivo);
                    using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                    {
                        await usuarioDTO.Foto.CopyToAsync(stream);
                    }
                    var rutaRelativa = Path.Combine("imagenes", nombreArchivo).Replace("\\", "/");

                    usuarioDTO.Clave = usuario.Clave;
                    usuarioDTO.Salt = usuario.Salt;
                    usuario = mapper.Map(usuarioDTO, usuario);
                    usuario.Estado = usuarioDTO.Estado;
                    usuario.Foto = rutaCompleta;

                    context.Usuarios.Update(usuario);
                    await context.SaveChangesAsync();

                    var permisosExistentes = await context.UsuarioPermisos
                    .Where(up => up.Usuario == codigo)
                    .ToListAsync();

                    context.UsuarioPermisos.RemoveRange(permisosExistentes);
                    await context.SaveChangesAsync();

                    List<UsuarioPermiso> listaroles = await (from rol in context.Rols
                                                             join rolp in context.RolPermisos on rol.Codigo equals rolp.Rol
                                                             where rol.Estado == 1 && rol.Codigo == usuarioDTO.Rol
                                                             select new UsuarioPermiso
                                                             {
                                                                 Usuario = codigo,
                                                                 Permiso = rolp.Permiso,
                                                                 Busqueda = rolp.Busqueda,
                                                                 Insertar = rolp.Insertar,
                                                                 Reimpresion = rolp.Reimpresion,
                                                                 // Entity Framework automáticamente maneja las relaciones de navegación
                                                             }).ToListAsync();


                    context.UsuarioPermisos.AddRange(listaroles);


                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return "1";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ObjectResult(new { message = ex.Message.ToString() });
                }
            }
        }

        public async Task<ActionResult<string>> actualizarusuariosi(int codigo, [FromForm] CreacionUsuarioSIDTO usuariopermisoDTO)
        {

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.Codigo == codigo);
                    if (usuario == null)
                    {
                        return new ObjectResult(new { message = "No se encontro el registro para actualizar" });
                    }
                    usuariopermisoDTO.Clave = usuario.Clave;
                    usuariopermisoDTO.Salt = usuario.Salt;
                    usuario = mapper.Map(usuariopermisoDTO, usuario);
                    usuario.Estado = usuariopermisoDTO.Estado;

                    context.Usuarios.Update(usuario);
                    await context.SaveChangesAsync();

                    var permisosExistentes = await context.UsuarioPermisos
                    .Where(up => up.Usuario == codigo)
                    .ToListAsync();

                    context.UsuarioPermisos.RemoveRange(permisosExistentes);
                    await context.SaveChangesAsync();

                    List<UsuarioPermiso> listaroles = await (from rol in context.Rols
                                                             join rolp in context.RolPermisos on rol.Codigo equals rolp.Rol
                                                             where rol.Estado == 1 && rol.Codigo == usuariopermisoDTO.Rol
                                                             select new UsuarioPermiso
                                                             {
                                                                 Usuario = codigo,
                                                                 Permiso = rolp.Permiso,
                                                                 Busqueda = rolp.Busqueda,
                                                                 Insertar = rolp.Insertar,
                                                                 Reimpresion = rolp.Reimpresion,
                                                                 // Entity Framework automáticamente maneja las relaciones de navegación
                                                             }).ToListAsync();


                    context.UsuarioPermisos.AddRange(listaroles);


                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    return "1";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ObjectResult(new { message = ex.Message.ToString() });
                }
            }

        }

        public async Task<ActionResult<UsuarioModeloLogin>> verificar_usuario(string usuario)
        {
            try
            {
                UsuarioModeloLogin? usuarioid = new UsuarioModeloLogin();

                usuarioid = await (from usuarios in context.Usuarios
                                   where usuarios.NombreUsuario == usuario
                                   select new
                                   {
                                       Codigo = usuarios.Codigo,
                                       NombreUsuario = usuarios.NombreUsuario,
                                       Nombre = usuarios.Nombre,
                                       CorreoElectronico = usuarios.CorreoElectronico,
                                       Telefono = usuarios.Telefono,
                                       Clave = usuarios.Clave,
                                       Salt = usuarios.Salt,
                                       Estado = usuarios.Estado,
                                       Rol = usuarios.Rol,
                                       UsuarioRegistro = usuarios.UsuarioRegistro,
                                       FechaRegistro = usuarios.FechaRegistro,
                                       Foto = usuarios.Foto,
                                       Entidad = usuarios.Entidad,
                                       Sucursal = usuarios.Sucursal
                                   }).Select(concepto => new UsuarioModeloLogin
                                   {

                                       id = concepto.Codigo,
                                       username = concepto.NombreUsuario,
                                       first_name = concepto.Nombre,
                                       last_name = concepto.CorreoElectronico,
                                       phone = concepto.Telefono,
                                       password = "",
                                       email = concepto.CorreoElectronico,
                                       entidad = concepto.Entidad ?? 0,
                                       sucursal = concepto.Sucursal ?? 0,
                                       ruta = concepto.Foto


                                   }).FirstOrDefaultAsync();

                if (usuarioid == null)
                {
                    return new ObjectResult(new { message = "No se encontro el registro" });
                }

                if (string.IsNullOrEmpty(usuarioid.ruta) || !System.IO.File.Exists(usuarioid.ruta))
                {
                    return usuarioid;
                }
                else
                {
                    byte[] bytes = System.IO.File.ReadAllBytes(usuarioid.ruta);
                    string base64 = Convert.ToBase64String(bytes);
                    usuarioid.ruta = $"data:image/{Path.GetExtension(usuarioid.ruta).Replace(".", "")};base64,{base64}";
                }
                return usuarioid;
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        public async Task<ActionResult<List<RolPermisoDTO>>> obtenerrolpermiso(string usuario)
        {
            try
            {
                List<RolPermisoDTO> lista;

                lista = await (from usuarios in context.Usuarios
                               join
                                   usuariopermiso in context.UsuarioPermisos on usuarios.Codigo equals usuariopermiso.Usuario
                               join permiso in context.Permisos on usuariopermiso.Permiso equals permiso.Codigo
                               where usuarios.NombreUsuario == usuario
                               select new
                               {
                                   Descripcion = permiso.Nombre,
                                   Busqueda = usuariopermiso.Busqueda,
                                   Insertar = usuariopermiso.Insertar,
                                   Reimpresion = usuariopermiso.Reimpresion,

                               }).Select(concepto => new RolPermisoDTO
                               {

                                   Nombre = concepto.Descripcion,
                                   Busqueda = concepto.Busqueda,
                                   Insertar = concepto.Insertar,
                                   Reimpresion = concepto.Reimpresion,

                               }).ToListAsync();

                if (lista.Count == 0)
                {
                    return new ObjectResult(new { message = "No se encontro el registro" });
                }

                return lista;
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }


    }
}
