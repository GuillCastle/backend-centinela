using AutoMapper;
using Backend.DTOs.Cuadrillas;
using Backend.DTOs.Evento;
using Backend.DTOs.Usuario;
using Backend.DTOs.Utils;
using Backend.Entidades;
using DocumentFormat.OpenXml.Office.PowerPoint.Y2022.M03.Main;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Repositorios.Evento
{
    public class RepositorioEvento : IRepositorioEvento
    {
        private readonly ILogger<RepositorioEvento> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;


        public RepositorioEvento(ILogger<RepositorioEvento> logger, ApplicationDbContext context, IMapper mapper, IConfiguration configuration, IWebHostEnvironment env)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.configuration = configuration;
            this.env = env;
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
                                                                            
                                                select new
                                                    {
                                                        Codigo = eventos.Codigo,
                                                        Descripcion = eventos.Descripcion,
                                                        Usuario = usuarios.Nombre,
                                                        Estado = eventos.Estado == 1 ? "Activo" : "Inactivo",
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
                    select new EventoDTO
                    {
                        Codigo = e.Codigo,
                        Descripcion = e.Descripcion,
                        Usuario = e.Usuario.ToString(),
                        Estado = e.Estado == 1 ? "Activo" : "Inactivo",
                        FechaRegistro = e.FechaRegistro,
                    }
                )
                .Distinct()
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
                                                select new
                                                {
                                                    Codigo = eventos.Codigo,
                                                    Descripcion = eventos.Descripcion,
                                                    Usuario = usuarios.Nombre,
                                                    Estado = eventos.Estado == 1 ? "Activo" : "Inactivo",
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
