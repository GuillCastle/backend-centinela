using AutoMapper;
using Backend.DTOs;
using Backend.DTOs.Utils;
using Backend.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace Backend.Repositorios.EmpresaSucursal
{
    public class RepositorioEmpresaSucursal : IRepositorioEmpresaSucursal
    {
        private readonly ILogger<RepositorioEmpresaSucursal> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RepositorioEmpresaSucursal(ILogger<RepositorioEmpresaSucursal> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ActionResult<List<EmpresaDTO>>> get()
        {
            try
            {
                List<EmpresaDTO> listamarcas = await (from EmpresaSucursal in context.Empresas
                                                     select new
                                                     {
                                                         CodigoEmpresa = EmpresaSucursal.CodigoEmpresa,
                                                         Nombre = EmpresaSucursal.Nombre,
                                                         Nit = EmpresaSucursal.Nit,
                                                         Representante = EmpresaSucursal.Representante,
                                                         Regimen = EmpresaSucursal.Regimen,
                                                         AfiliacionIva = EmpresaSucursal.AfiliacionIva,
                                                         CorreoElectronico = EmpresaSucursal.CorreoElectronico,
                                                         Telefono = EmpresaSucursal.Telefono,
                                                         Estado = EmpresaSucursal.Estado == 1 ? "Activo" : "Inactivo"
                                                     }).Select(concepto => new EmpresaDTO
                                                     {
                                                         CodigoEmpresa = concepto.CodigoEmpresa,
                                                         Nombre = concepto.Nombre,
                                                         Nit = concepto.Nit,
                                                         Representante = concepto.Representante,
                                                         Regimen = concepto.Regimen,
                                                         AfiliacionIva = concepto.AfiliacionIva,
                                                         CorreoElectronico = concepto.CorreoElectronico,
                                                         Telefono = concepto.Telefono,
                                                         Estado = concepto.Estado
                                                     }).ToListAsync();

                return listamarcas;
            }
            catch (Exception ex)
            {
                return new ObjectResult(JObject.Parse(ex.Message.ToString()));
            }
        }
        public async Task<ActionResult<EmpresaDTO>> getid(int codigo)
        {
            try
            {
                EmpresaDTO? marcas = new EmpresaDTO();

                marcas = await (from EmpresaSucursal in context.Empresas
                                where EmpresaSucursal.Codigo == codigo
                                select new
                                {
                                    CodigoEmpresa = EmpresaSucursal.CodigoEmpresa,
                                    Nombre = EmpresaSucursal.Nombre,
                                    Nit = EmpresaSucursal.Nit,
                                    Representante = EmpresaSucursal.Representante,
                                    Regimen = EmpresaSucursal.Regimen,
                                    AfiliacionIva = EmpresaSucursal.AfiliacionIva,
                                    CorreoElectronico = EmpresaSucursal.CorreoElectronico,
                                    Telefono = EmpresaSucursal.Telefono,
                                    Estado = EmpresaSucursal.Estado == 1 ? "Activo" : "Inactivo",
                                    Nivel = EmpresaSucursal.Nivel
                                }).Select(concepto => new EmpresaDTO
                                {
                                    CodigoEmpresa = concepto.CodigoEmpresa,
                                    Nombre = concepto.Nombre,
                                    Nit = concepto.Nit,
                                    Representante = concepto.Representante,
                                    Regimen = concepto.Regimen,
                                    AfiliacionIva = concepto.AfiliacionIva,
                                    CorreoElectronico = concepto.CorreoElectronico,
                                    Telefono = concepto.Telefono,
                                    Estado = concepto.Estado.ToString(),
                                    Nivel = concepto.Nivel ?? 0
                                }).FirstOrDefaultAsync();

                if(marcas == null)
                {
                    return new ObjectResult(new { message = "No se encontró el registro" });
                }

                return marcas;
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        public async Task<ActionResult<string>> post([FromBody] CreacionEmpresaDTO creacionEmpresa)
        {
            try
            {
                using var transaction = context.Database.BeginTransaction();

                var marca = mapper.Map<Empresa>(creacionEmpresa);
                if (marca == null)
                {
                    return "Empresa se encuentra vacia";
                }
                context.Add(marca);
                await context.SaveChangesAsync();
                transaction.Commit();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public async Task<ActionResult<string>> put(int codigo, [FromBody] CreacionEmpresaDTO empresaEdicion)
        {
            try
            {
                using var transaction = context.Database.BeginTransaction();
                var empresa = await context.Empresas.FirstOrDefaultAsync(x => x.Codigo == codigo);
                if (empresa == null)
                {
                    return new ObjectResult(JObject.Parse("No se encontro el registro a actualizar"));
                }

                empresa = mapper.Map(empresaEdicion, empresa);
                empresa.Estado = 1;
                await context.SaveChangesAsync();
                transaction.Commit();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }
        public async Task<ActionResult<string>> delete(int codigo)
        {
            try
            {
                using var transaction = context.Database.BeginTransaction();
                var empresaInactivar = await context.Empresas.FirstOrDefaultAsync(x => x.Codigo == codigo);
                var empresa = await context.Empresas.FirstOrDefaultAsync(x => x.Codigo == codigo);
                if (empresa == null)
                {
                    return new ObjectResult(JObject.Parse("No se encontro el registro a desactivar"));
                }

                empresa = mapper.Map(empresaInactivar, empresa);

                empresa.Estado = 0;

                await context.SaveChangesAsync();
                transaction.Commit();
                return "1";
            }
            catch (Exception ex)
            {
                return new ObjectResult(JObject.Parse(ex.Message.ToString()));
            }
        }

        public async Task<ActionResult<List<SucursalDTO>>> getSucursal()
        {
            try
            {
                //List<SucursalDTO> listasucursal = await context.Sucursales.Include(e => e.Empresa)
                //    .OrderBy(e => e.Codigo)
                //    .ToListAsync();

                List<SucursalDTO> listasucursal = await (from sucursal in context.Sucursales 
                                                         join empresa in context.Empresas on sucursal.EmpresaCodigo equals empresa.Codigo select new
                                                         {
                                                             Codigo = sucursal.Codigo,
                                                             Empresa = sucursal.EmpresaCodigo,
                                                             NombreEmpresa = empresa.Nombre,
                                                             CodigoSucursal = sucursal.CodigoSucursal,
                                                             Descripcion = sucursal.Descripcion,
                                                             Abreviatura = sucursal.Abreviatura,
                                                             Encargado = sucursal.Encargado,
                                                             Telefono = sucursal.Telefono,
                                                             CorreoElectronico = sucursal.CorreoElectronico,
                                                             Estado = sucursal.Estado == 1 ? "Activo" : "Inactivo",
                                                             CorreoCopia = sucursal.CorreoCopia,
                                                             LlaveSucursal = sucursal.LlaveSucursal,
                                                             LlaveFirma = sucursal.LlaveFirma

                                                         }).Select(concepto => new SucursalDTO
                                                         {
                                                             Codigo = concepto.Codigo,
                                                             Empresa = concepto.Empresa,
                                                             NombreEmpresa = concepto.NombreEmpresa,
                                                             CodigoSucursal = concepto.CodigoSucursal,
                                                             Descripcion = concepto.Descripcion,
                                                             Abreviatura = concepto.Abreviatura,
                                                             Encargado = concepto.Encargado,
                                                             Telefono = concepto.Telefono,
                                                             CorreoElectronico = concepto.CorreoElectronico,
                                                             Estado = concepto.Estado,
                                                             CorreoCopia = concepto.CorreoCopia,
                                                             LLaveSucursal = concepto.LlaveSucursal,
                                                             LlaveFirma = concepto.LlaveFirma
                                                         }).ToListAsync();

                return listasucursal;
            }
            catch (Exception ex)
            {
                return new ObjectResult(JObject.Parse(ex.Message.ToString()));
            }
        }
        public async Task<ActionResult<SucursalDTO>> getSucursalid(int codigo)
        {
            try
            {
                SucursalDTO? sucursalIndividual = new SucursalDTO();

                sucursalIndividual = await (from sucursal in context.Sucursales
                                  join empresa in context.Empresas on sucursal.EmpresaCodigo equals empresa.Codigo
                                  where sucursal.Codigo == codigo
                                  select new
                                  {
                                      Codigo = sucursal.Codigo,
                                      Empresa = sucursal.EmpresaCodigo,
                                      NombreEmpresa = empresa.Nombre,
                                      CodigoSucursal = sucursal.CodigoSucursal,
                                      Descripcion = sucursal.Descripcion,
                                      Abreviatura = sucursal.Abreviatura,
                                      Encargado = sucursal.Encargado,
                                      Telefono = sucursal.Telefono,
                                      CorreoElectronico = sucursal.CorreoElectronico,
                                      Estado = sucursal.Estado,
                                      CorreoCopia = sucursal.CorreoCopia,
                                      LlaveSucursal = sucursal.LlaveSucursal,
                                      LlaveFirma = sucursal.LlaveFirma

                                  }).Select(concepto => new SucursalDTO
                                  {
                                      Codigo = concepto.Codigo,
                                      Empresa = concepto.Empresa,
                                      NombreEmpresa = concepto.NombreEmpresa,
                                      CodigoSucursal = concepto.CodigoSucursal,
                                      Descripcion = concepto.Descripcion,
                                      Abreviatura = concepto.Abreviatura,
                                      Encargado = concepto.Encargado,
                                      Telefono = concepto.Telefono,
                                      CorreoElectronico = concepto.CorreoElectronico,
                                      Estado = concepto.Estado.ToString(),
                                      CorreoCopia = concepto.CorreoCopia,
                                      LLaveSucursal = concepto.LlaveSucursal,
                                      LlaveFirma = concepto.LlaveFirma
                                  }).FirstOrDefaultAsync();


                return sucursalIndividual;
            }
            catch (Exception ex)
            {
                return new ObjectResult(JObject.Parse(ex.Message.ToString()));
            }
        }

        public async Task<ActionResult<string>> postSucursal([FromBody] CreacionSucursalDTO creacionSucursal)
        {
            try
            {
                using var transaction = context.Database.BeginTransaction();

                var marca = mapper.Map<Sucursale>(creacionSucursal);
                if (marca == null)
                {
                    return "Empresa se encuentra vacia";
                }
                context.Add(marca);
                await context.SaveChangesAsync();
                transaction.Commit();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public async Task<ActionResult<string>> putSucursal(int codigo, [FromBody] CreacionSucursalDTO sucursalEdicion)
        {
            try
            {
                using var transaction = context.Database.BeginTransaction();
                var sucursal = await context.Sucursales.FirstOrDefaultAsync(x => x.Codigo == codigo);
                if (sucursal == null)
                {
                    return new ObjectResult(JObject.Parse("No se encontro el registro a actualizar"));
                }

                sucursal = mapper.Map(sucursalEdicion, sucursal);
                sucursal.Estado = 1;
                await context.SaveChangesAsync();
                transaction.Commit();
                return "1";
            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        public async Task<ActionResult<List<SelectFormulario>>> selectentidad()
        {
            try
            {
                List<SelectFormulario> lista = await context.Empresas
                                            .Where(empresa => empresa.Estado == 1) // Filtro por Estado == 1
                                            .Select(empresa => new SelectFormulario // Proyección a ListaCmbRol
                                            {
                                                codigo = empresa.Codigo,
                                                descripcion = empresa.CodigoEmpresa + " " + empresa.Nombre,
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

        public async Task<ActionResult<List<SelectFormulario>>> selectsucursal(int empresa)
        {
            try
            {
                List<SelectFormulario> lista = await context.Sucursales
                                            .Where(sucursal => sucursal.Estado == 1 && sucursal.EmpresaCodigo == empresa) // Filtro por Estado == 1
                                            .Select(sucursal => new SelectFormulario // Proyección a ListaCmbRol
                                            {
                                                codigo = sucursal.Codigo,
                                                descripcion = sucursal.CodigoSucursal + " " + sucursal.Descripcion,
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

    }
}
