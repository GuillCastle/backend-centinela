using AutoMapper;
using Backend.DTOs.Cuadrillas;
using Backend.DTOs.Evento;
using Backend.DTOs.Usuario;
using Backend.DTOs.Utils;
using Backend.Entidades;
using DocumentFormat.OpenXml.Office.PowerPoint.Y2022.M03.Main;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;
using Backend.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Backend.Repositorios.Evento
{
    public class RepositorioEvento : IRepositorioEvento
    {
        private readonly ILogger<RepositorioEvento> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;
        private readonly IHubContext<NotificacionHub> hubContext;


        public RepositorioEvento(ILogger<RepositorioEvento> logger, ApplicationDbContext context, IMapper mapper, IConfiguration configuration, IWebHostEnvironment env, IHubContext<NotificacionHub> hubContext)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.configuration = configuration;
            this.env = env;
            this.hubContext = hubContext;
        }

        public async Task<ActionResult<EncabezadoDatos>> post([FromForm] CreacionEventoGeneralDTO Creacion)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var apertura = await context.AperturaCampanaElectorals.FirstOrDefaultAsync(x => x.Estado == 1);
                    if (apertura == null)
                    {
                        return new ObjectResult(new { message = "No cuenta con una apertura de evento electoral" });
                    }
                    var Encabezado = mapper.Map<Entidades.Evento>(Creacion.EncabezadoEvento);
                    if (Encabezado == null)
                    {
                        return new ObjectResult(new { message = "No cuenta con el encabezado del evento" });
                    }
                    var municipio = await context.Municipios
                    .FirstOrDefaultAsync(x => x.Codigo == Creacion.Municipio);

                    if (municipio == null)
                    {
                        return new ObjectResult(new { message = "No se encontró el municipio" });
                    }

                    Encabezado.AperturaCampanaElectoral = apertura.Codigo;
                    Encabezado.PuntoGeografico = municipio.PuntoGeografico;
                    Encabezado.Departamento = Creacion.Departamento;
                    Encabezado.Municipio = Creacion.Municipio;
                    Encabezado.TipoEvento = Creacion.TipoEvento;
                    context.Add(Encabezado);
                    await context.SaveChangesAsync();

                    var Detalle = mapper.Map<Entidades.DetalleEvento>(Creacion.DetalleEvento);
                    if (Detalle == null)
                    {
                        return new ObjectResult(new { message = "No cuenta con el detalle del evento" });
                    }
                    Detalle.Evento = Encabezado.Codigo;
                    context.Add(Detalle);
                    await context.SaveChangesAsync();


                    if (Creacion.Documentos != null && Creacion.Documentos.Count > 0)
                    {
                        // ruta base (dentro del proyecto)
                        string carpetaBase = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot",
                            "eventos"
                        );

                        // crear carpeta si no existe
                        if (!Directory.Exists(carpetaBase))
                        {
                            Directory.CreateDirectory(carpetaBase);
                        }

                        foreach (var archivo in Creacion.Documentos)
                        {
                            if (archivo != null && archivo.Length > 0)
                            {

                                var nombreArchivo = $"{Guid.NewGuid()}{Path.GetExtension(archivo.FileName)}";


                                var rutaCompleta = Path.Combine(carpetaBase, nombreArchivo);


                                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                                {
                                    await archivo.CopyToAsync(stream);
                                }


                                var documento = new DetalleEventoDocumento
                                {
                                    DetalleEvento = Detalle.Codigo,
                                    Url = Path.Combine("eventos", nombreArchivo).Replace("\\", "/"), 
                                    Estado = 1,
                                    FechaRegistro = DateTime.Now
                                };

                                context.Add(documento);
                            }
                        }

                        await context.SaveChangesAsync();
                    }

                    // ============================
                    // OBTENER SUPERVISORES A NOTIFICAR
                    // ============================
                    var idUsuarioEvento = Encabezado.Usuario;

                    var cuadrillasDelUsuario = await context.DetalleUsuarioCuadrillas
                        .Where(x => x.Usuario == idUsuarioEvento && x.Estado == 1)
                        .Select(x => x.Cuadrilla)
                        .Distinct()
                        .ToListAsync();

                    var supervisores = await context.DetalleUsuarioCuadrillas
                        .Where(x => cuadrillasDelUsuario.Contains(x.Cuadrilla)
                                 && x.Estado == 1
                                 && x.TipoCuadrilla == 2)
                        .Select(x => x.Usuario)
                        .Distinct()
                        .ToListAsync();

                    // ============================
                    // GUARDAR NOTIFICACIONES EN BD
                    // ============================
                    var tituloNotificacion = "Nuevo evento reportado";
                    var mensajeNotificacion = $"Se ha levantado un nuevo evento: {Encabezado.Descripcion}";

                    foreach (var supervisor in supervisores)
                    {
                        var notificacion = new Notificacion
                        {
                            Usuario = supervisor,
                            Titulo = tituloNotificacion,
                            Mensaje = mensajeNotificacion,
                            Tipo = "EVENTO_NUEVO",
                            Evento = Encabezado.Codigo,
                            Estado = 1,
                            Leida = 0,
                            FechaRegistro = DateTime.Now
                        };

                        context.Add(notificacion);
                    }

                    await context.SaveChangesAsync();


                    // ============================
                    // ENVIAR EN TIEMPO REAL
                    // ============================
                    foreach (var supervisor in supervisores)
                    {
                        await hubContext.Clients.Group($"usuario_{supervisor}")
                            .SendAsync("RecibirNotificacion", new
                            {
                                titulo = tituloNotificacion,
                                mensaje = mensajeNotificacion,
                                tipo = "EVENTO_NUEVO",
                                referenciaId = Encabezado.Codigo,
                                fechaRegistro = DateTime.Now
                            });
                    }

                    await transaction.CommitAsync();



                    EncabezadoDatos datos = new EncabezadoDatos();
                    datos.id = int.Parse(Encabezado.Codigo.ToString());
                    datos.mensaje = "1";
                    return datos;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ObjectResult(new { message = ex.Message.ToString() });
                }
            }
        }

        public async Task<ActionResult<List<EventoDTO>>> obtenereventousuariocentinela(int usuario)
        {
            try
            {
               
                List<EventoDTO> evento = await (from eventos in context.Eventos
                                                join usuarios in context.Usuarios on eventos.Usuario equals usuarios.Codigo
                                                where eventos.Usuario == usuario
                                                orderby eventos.Codigo descending
                                                                            
                                                select new
                                                    {
                                                        Codigo = eventos.Codigo,
                                                        Descripcion = eventos.Descripcion,
                                                        Usuario = usuarios.Nombre,
                                                        Estado = 
                                                        eventos.Estado == 1 ? "Enviado al Supervisor" :
                                                        eventos.Estado == 2 ? "Enviado al Administrador":
                                                        eventos.Estado == 3 ? "Finalizado y con Respuesta"
                                                        : "Inactivo",
                                                        FechaRegistro = eventos.FechaRegistro,
                                                    }).Select(concepto => new EventoDTO
                                                    {
                                                        Codigo = concepto.Codigo,
                                                        Descripcion = concepto.Descripcion,
                                                        Usuario = concepto.Usuario,
                                                        Estado = concepto.Estado,
                                                        FechaRegistro = concepto.FechaRegistro,
                                                    }).ToListAsync();

                

                return evento;
            }
            catch (Exception ex)
            {
                return new ObjectResult(JObject.Parse(ex.Message.ToString()));
            }
        }

        public async Task<IActionResult> descargararchivo(long id)
        {
            try
            {
                var documento = await context.DetalleEventoDocumentos
                    .FirstOrDefaultAsync(x => x.Codigo == id && x.Estado == 1);

                if (documento == null)
                {
                    return new NotFoundObjectResult(new { message = "No se encontró el documento" });
                }

                string rutaBase = Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "wwwroot"
                );

                string rutaCompleta = Path.Combine(
                    rutaBase,
                    documento.Url.Replace("/", Path.DirectorySeparatorChar.ToString())
                );

                if (!System.IO.File.Exists(rutaCompleta))
                {
                    return new NotFoundObjectResult(new { message = "El archivo no existe físicamente en el servidor" });
                }

                var bytes = await System.IO.File.ReadAllBytesAsync(rutaCompleta);
                var contentType = ObtenerContentType(rutaCompleta);
                var nombreArchivo = Path.GetFileName(rutaCompleta);

                return new FileContentResult(bytes, contentType)
                {
                    FileDownloadName = nombreArchivo
                };
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message });
            }
        }

        public async Task<ActionResult<List<EventoDTO>>> obtenereventousuariosupervisor(int usuario)
        {
            try
            {

                List<EventoDTO> eventos = await (
                    from e in context.Eventos
                    join ducEvento in context.DetalleUsuarioCuadrillas
                        on e.Usuario equals ducEvento.Usuario
                    join ducViewer in context.DetalleUsuarioCuadrillas
                        on ducEvento.Cuadrilla equals ducViewer.Cuadrilla
                    where ducEvento.Estado == 1
                          && ducViewer.Estado == 1
                          && ducViewer.TipoCuadrilla == 2
                          && ducViewer.Usuario == usuario
                          orderby e.Codigo descending
                    select new EventoDTO
                    {
                        Codigo = e.Codigo,
                        Descripcion = e.Descripcion,
                        Usuario = e.Usuario.ToString(),
                        Estado =
                        e.Estado == 1 ? "Enviado al Supervisor" :
                        e.Estado == 2 ? "Enviado al Administrador" :
                        e.Estado == 3 ? "Finalizado y con Respuesta"
                        : "Inactivo",
                        FechaRegistro = e.FechaRegistro,
                    }
                )
                .Distinct()
                .OrderByDescending(x => x.Codigo)
                .ToListAsync();



                return eventos;
            }
            catch (Exception ex)
            {
                return new ObjectResult(JObject.Parse(ex.Message.ToString()));
            }
        }

        public async Task<ActionResult<List<EventoDTO>>> obtenereventousuarioadministrador(int usuario)
        {
            try
            {

                List<EventoDTO> evento = await (from eventos in context.Eventos
                                                join usuarios in context.Usuarios on eventos.Usuario equals usuarios.Codigo
                                                where eventos.Estado > 1
                                                orderby eventos.Codigo descending
                                                select new
                                                {
                                                    Codigo = eventos.Codigo,
                                                    Descripcion = eventos.Descripcion,
                                                    Usuario = usuarios.Nombre,
                                                    Estado = 
                                                    eventos.Estado == 1 ? "Enviado al Supervisor" :
                                                    eventos.Estado == 2 ? "Con el Administrador" :
                                                    eventos.Estado == 3 ? "Finalizado"
                                                    : "Inactivo",
                                                    FechaRegistro = eventos.FechaRegistro,
                                                }).Select(concepto => new EventoDTO
                                                {
                                                    Codigo = concepto.Codigo,
                                                    Descripcion = concepto.Descripcion,
                                                    Usuario = concepto.Usuario,
                                                    Estado = concepto.Estado,
                                                    FechaRegistro = concepto.FechaRegistro,
                                                }).ToListAsync();



                return evento;
            }
            catch (Exception ex)
            {
                return new ObjectResult(JObject.Parse(ex.Message.ToString()));
            }
        }

        public async Task<ActionResult<EventoGeneralDTO>> obtenereventodetalle(int evento)
        {
            try
            {
                var even = await context.Eventos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Codigo == evento);

                if (even == null)
                {
                    return new ObjectResult(new { message = "Evento no encontrado" });
                }

                var detalleeven = await context.DetalleEventos
                    .AsNoTracking()
                    .Where(x => x.Evento == evento)
                    .ToListAsync();

                var detalleIds = detalleeven.Select(x => x.Codigo).ToList();

                var documentos = await context.DetalleEventoDocumentos
                    .AsNoTracking()
                    .Where(x => detalleIds.Contains(x.DetalleEvento))
                    .ToListAsync();

                var eventoGenerado = new EventoGeneralDTO
                {
                    EncabezadoEvento = new EventoIdDTO
                    {
                        Codigo = even.Codigo,
                        Descripcion = even.Descripcion,
                        Estado = even.Estado,
                        Usuario = even.Usuario,
                        FechaRegistro = even.FechaRegistro,
                        AperturaCampanaElectoral = even.AperturaCampanaElectoral,
                        Departamento = even.Departamento,
                        Municipio = even.Municipio,
                        TipoEvento = even.TipoEvento
                    },
                    DetalleEventoGenerado = detalleeven.Select(x => new DetalleEventoDTO
                    {
                        Codigo = x.Codigo,
                        Evento = x.Evento,
                        Descripcion = x.Descripcion,
                        Usuario = x.Usuario,
                        FechaRegistro = x.FechaRegistro,
                        TipoCuadrilla = x.TipoCuadrilla
                    }).ToList(),
                    DetalleDocumentoGenerado = documentos.Select(x => new DetalleEventoDocumentoDTO
                    {
                        Codigo = x.Codigo,
                        DetalleEvento = x.DetalleEvento,
                        Url = x.Url,
                        Estado = x.Estado,
                        FechaRegistro = x.FechaRegistro
                    }).ToList()
                };

                return eventoGenerado;
            }
            catch (Exception ex)
            {
                return new ObjectResult(JObject.Parse(ex.Message.ToString()));
            }
        }

        public async Task<ActionResult<EncabezadoDatos>> insertarsolodetalleevento([FromBody] CreacionDetalleEventoDTO Creacion)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var evento = await context.Eventos
                    .FirstOrDefaultAsync(x => x.Codigo == Creacion.Evento);

                    if (evento == null)
                    {
                        return new NotFoundObjectResult(new { message = "No se encontró el evento" });
                    }

                    evento.Estado = 2;
                    await context.SaveChangesAsync();


                    var Detalle = mapper.Map<Entidades.DetalleEvento>(Creacion);
                    context.Add(Detalle);
                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                    EncabezadoDatos datos = new EncabezadoDatos();
                    datos.id = int.Parse(Detalle.Codigo.ToString());
                    datos.mensaje = "1";
                    return datos;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ObjectResult(new { message = ex.Message.ToString() });
                }
            }
        }

        public async Task<ActionResult<EncabezadoDatos>> insertarsolodetalleeventoadministrador([FromForm] CreacionEventoGeneralAdministradorDTO Creacion)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {

                    var evento = await context.Eventos
                    .FirstOrDefaultAsync(x => x.Codigo == Creacion.DetalleEvento.Evento);

                    if (evento == null)
                    {
                        return new NotFoundObjectResult(new { message = "No se encontró el evento" });
                    }

                    evento.Estado = 3;
                    await context.SaveChangesAsync();


                    var Detalle = mapper.Map<Entidades.DetalleEvento>(Creacion.DetalleEvento);
                    if (Detalle == null)
                    {
                        return new ObjectResult(new { message = "No cuenta con el detalle del evento" });
                    }
                    context.Add(Detalle);
                    await context.SaveChangesAsync();


                    if (Creacion.Documentos != null && Creacion.Documentos.Count > 0)
                    {
                        // ruta base (dentro del proyecto)
                        string carpetaBase = Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "wwwroot",
                            "eventos"
                        );

                        // crear carpeta si no existe
                        if (!Directory.Exists(carpetaBase))
                        {
                            Directory.CreateDirectory(carpetaBase);
                        }

                        foreach (var archivo in Creacion.Documentos)
                        {
                            if (archivo != null && archivo.Length > 0)
                            {

                                var nombreArchivo = $"{Guid.NewGuid()}{Path.GetExtension(archivo.FileName)}";


                                var rutaCompleta = Path.Combine(carpetaBase, nombreArchivo);


                                using (var stream = new FileStream(rutaCompleta, FileMode.Create))
                                {
                                    await archivo.CopyToAsync(stream);
                                }


                                var documento = new DetalleEventoDocumento
                                {
                                    DetalleEvento = Detalle.Codigo,
                                    Url = Path.Combine("eventos", nombreArchivo).Replace("\\", "/"),
                                    Estado = 1,
                                    FechaRegistro = DateTime.Now
                                };

                                context.Add(documento);
                            }
                        }

                        await context.SaveChangesAsync();
                    }


                    await transaction.CommitAsync();
                    EncabezadoDatos datos = new EncabezadoDatos();
                    datos.id = int.Parse(Creacion.DetalleEvento.Evento.ToString());
                    datos.mensaje = "1";
                    return datos;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new ObjectResult(new { message = ex.Message.ToString() });
                }
            }
        }

        public async Task<ActionResult<EncabezadoDatos>> delete(int codigo)
        {
            try
            {

                using var transaction = await context.Database.BeginTransactionAsync();

                var apertura = await context.Eventos
                    .FirstOrDefaultAsync(x => x.Codigo == codigo);

                if (apertura == null)
                {
                    return new ObjectResult(new { message = "No se encontro el registro a desactivar" });
                }

                apertura.Estado = 0;
                await context.SaveChangesAsync();

                transaction.Commit();
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
        private string ObtenerContentType(string rutaArchivo)
        {
            var extension = Path.GetExtension(rutaArchivo).ToLowerInvariant();

            switch (extension)
            {
                case ".pdf":
                    return "application/pdf";
                case ".xls":
                    return "application/vnd.ms-excel";
                case ".xlsx":
                    return "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                case ".doc":
                    return "application/msword";
                case ".docx":
                    return "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".txt":
                    return "text/plain";
                default:
                    return "application/octet-stream";
            }
        }

    }
}
