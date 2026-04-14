using AutoMapper;
using Backend.DTOs.AperturaCampanaElectoral;
using Backend.DTOs.Utils;
using Backend.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Backend.Repositorios.AperturaCampanaElectoral
{
    public class RepositorioAperturaCampanaElectoral : IRepositorioAperturaCampanaElectoral
    {
        private readonly ILogger<RepositorioAperturaCampanaElectoral> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RepositorioAperturaCampanaElectoral(ILogger<RepositorioAperturaCampanaElectoral> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ActionResult<List<AperturaCampanaElectoralDTO>>> get()
        {
            try
            {
                List<AperturaCampanaElectoralDTO> entity = await (from aperturacamapana in context.AperturaCampanaElectorals
                                             select new
                                             {
                                                 Codigo = aperturacamapana.Codigo,
                                                 Descripcion = aperturacamapana.Descripcion,
                                                 Estado = aperturacamapana.Estado == 1 ? "Activo" : "Inactivo",
                                                 FechaInicio = aperturacamapana.FechaInicio,
                                                 FechaFin = aperturacamapana.FechaFin
                                             }).Select(concepto => new AperturaCampanaElectoralDTO
                                             {
                                                 Codigo = concepto.Codigo,
                                                 Descripcion = concepto.Descripcion,
                                                 Estado = concepto.Estado,
                                                 FechaInicio = concepto.FechaInicio,
                                                 FechaFin = concepto.FechaFin
                                             }).ToListAsync();

                return entity;
            }
            catch (Exception ex)
            {
                return new ObjectResult(JObject.Parse(ex.Message.ToString()));
            }
        }

        public async Task<ActionResult<Backend.Entidades.AperturaCampanaElectoral>> getid(int codigo)
        {
            try
            {
                var dato = await context.AperturaCampanaElectorals.FirstOrDefaultAsync(x => x.Codigo == codigo);

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

        public async Task<ActionResult<EncabezadoDatos>> post([FromBody] CreacionAperturaCampanaElectoralDTO Creacion)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {

                    var validacion = await context.AperturaCampanaElectorals
                    .Where(x => x.Estado == 1)
                    .ToListAsync();
                    if (validacion.Count > 0)
                    {
                        return new ObjectResult(new { message = "Existe una campaña electoral activa" });
                    }
                    var Nueva = mapper.Map<Entidades.AperturaCampanaElectoral>(Creacion);
                    if (Nueva == null)
                    {
                        return new ObjectResult(new { message = "No cuenta con datos la apertura de campaña electoral" });
                    }
                    context.Add(Nueva);
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

        public async Task<ActionResult<EncabezadoDatos>> put(int codigo, [FromBody] CreacionAperturaCampanaElectoralDTO Edicion)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var validacion = await context.AperturaCampanaElectorals
                    .Where(x => x.Estado == 1 && x.Codigo != codigo)
                    .ToListAsync();
                    if (validacion.Count > 0)
                    {
                        return new ObjectResult(new { message = "Existe una campaña electoral activa" });
                    }
                    var nuevo = await context.AperturaCampanaElectorals.FirstOrDefaultAsync(x => x.Codigo == codigo);
                    if (nuevo == null)
                    {
                        return new ObjectResult(new { message = "No se encontro el registro a actualizar" });
                    }
                    nuevo = mapper.Map(Edicion, nuevo);
                    //compra.Estado = compraEdicion.Estado;
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

        public async Task<ActionResult<EncabezadoDatos>> delete(int codigo)
        {
            try
            {
                using var transaction = context.Database.BeginTransaction();
                var Inactivar = await context.AperturaCampanaElectorals.FirstOrDefaultAsync(x => x.Codigo == codigo);
                var apertura = await context.AperturaCampanaElectorals.FirstOrDefaultAsync(x => x.Codigo == codigo);
                if (apertura == null)
                {
                    return new ObjectResult(new { message = "No se encontro el registro a desactivar" });
                }

                apertura = mapper.Map(Inactivar, apertura);

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

        public async Task<ActionResult<List<SelectFormulario>>> selectapertura()
        {
            try
            {
                List<SelectFormulario> entity = await (from aperturacamapana in context.AperturaCampanaElectorals
                                                                  select new
                                                                  {
                                                                      Codigo = aperturacamapana.Codigo,
                                                                      Descripcion = aperturacamapana.Descripcion,
                                                                  }).Select(concepto => new SelectFormulario
                                                                  {
                                                                      codigo = concepto.Codigo,
                                                                      descripcion = concepto.Descripcion,
                                                                  }).ToListAsync();

                return entity;
            }
            catch (Exception ex)
            {
                return new ObjectResult(JObject.Parse(ex.Message.ToString()));
            }
        }

    }
}
