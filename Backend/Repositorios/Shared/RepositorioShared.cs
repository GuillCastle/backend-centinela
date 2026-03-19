using AutoMapper;
using Backend.DTOs;
using Backend.DTOs.Clientes;
using Backend.DTOs.Proveedores;
using Backend.DTOs.Shared;
using Backend.DTOs.Utils;
using Backend.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Backend.Repositorios.Shared
{
    public class RepositorioShared : IRepositorioShared
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RepositorioShared> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RepositorioShared(ILogger<RepositorioShared> logger, ApplicationDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<ActionResult<List<SelectFormulario>>> obtenerDepartamento(int pais)
        {
            try
            {
                List<SelectFormulario> lista = await (from departamento in context.Departamentos where departamento.Pais == pais
                                                      select new
                                                      {
                                                          Codigo = departamento.Codigo,
                                                          Descripcion = departamento.Descripcion
                                                      }).Select(concepto => new SelectFormulario
                                                      {
                                                          codigo = Convert.ToInt32(concepto.Codigo),
                                                          descripcion = concepto.Descripcion,
                                                      }).ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        public async Task<ActionResult<List<SelectFormulario>>> obtenerMunicipio(int departamento)
        {
            try
            {
                List<SelectFormulario> lista = await (from Municipio in context.Municipios where Municipio.Departamento == departamento
                                                      select new
                                                      {
                                                          Codigo = Municipio.Codigo,
                                                          Descripcion = Municipio.Descripcion
                                                      }).Select(concepto => new SelectFormulario
                                                      {
                                                          codigo = Convert.ToInt32(concepto.Codigo),
                                                          descripcion = concepto.Descripcion,
                                                      }).ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        public async Task<ActionResult<RespuestaNitDpiInfileDTO>> obtenerdatoscertificadornit(string nit, int entidad, int sucursal, int tipo)
        {
            try
            {
                if (tipo == 1)
                {
                    if (nit.ToUpper() == "CF")
                    {
                        RespuestaNitDpiInfileDTO respuestaCF = new RespuestaNitDpiInfileDTO();
                        respuestaCF.nit = "CF";
                        respuestaCF.nombre = "";
                        respuestaCF.respuesta = "1";
                        return respuestaCF;
                    }

                    ClientesIdDTO? clientes = new ClientesIdDTO();
                    clientes = await (from cliente in context.Clientes
                                      where cliente.Nit == nit
                                      select new
                                      {
                                          nit = cliente.Nit,
                                      }).Select(concepto => new ClientesIdDTO
                                      {
                                          Nit = concepto.nit
                                      }).FirstOrDefaultAsync();
                    if (clientes != null)
                    {
                        return new ObjectResult(new { message = "Este NIT ya se encuentra en uso" });
                    }
                    else
                    {
                        RespuestaNitDpiInfileDTO respuesta = new RespuestaNitDpiInfileDTO();
                        EmpresaDTO? empresas = new EmpresaDTO();
                        empresas = await (from empre in context.Empresas
                                          where empre.Codigo == entidad
                                          select new
                                          {
                                              alias = empre.Alias,

                                          }).Select(concepto => new EmpresaDTO
                                          {
                                              Alias = concepto.alias,
                                          }).FirstOrDefaultAsync();

                        SucursalDTO? sucursales = new SucursalDTO();
                        sucursales = await (from sucur in context.Sucursales
                                            where sucur.Codigo == sucursal
                                            select new
                                            {
                                                codigo = sucur.Codigo,
                                                llavesucursal = sucur.LlaveSucursal,
                                                llavefirma = sucur.LlaveFirma

                                            }).Select(concepto => new SucursalDTO
                                            {
                                                Codigo = concepto.codigo,
                                                LLaveSucursal = concepto.llavesucursal,
                                                LlaveFirma = concepto.llavefirma
                                            }).FirstOrDefaultAsync();

                        var url = "https://certificador.feel.com.gt/api/v2/servicios/externos/login";

                        var httpClient = new HttpClient();

                        var valores = new Dictionary<string, string>
                {
                    { "prefijo", empresas.Alias },
                    { "llave", sucursales.LlaveFirma }
                };

                        var content = new FormUrlEncodedContent(valores);

                        HttpResponseMessage response = await httpClient.PostAsync(url, content);

                        string resultContent = await response.Content.ReadAsStringAsync();
                        var loginResponse = JsonSerializer.Deserialize<RespuestaDatosClientesProveedores>(resultContent);
                        if (loginResponse == null)
                        {
                            return new ObjectResult(new { message = "Autenticación fallo con infile" });
                        }
                        else
                        {
                            if (loginResponse.resultado == false)
                            {
                                return new ObjectResult(new { message = loginResponse.descripcion });
                            }
                            else
                            {
                                string token = loginResponse.token;
                                var urlNit = "https://consultareceptores.feel.com.gt/rest/action";
                                var data = new Dictionary<string, string>
                        {
                            { "emisor_codigo", empresas.Alias },
                            { "emisor_clave", sucursales.LlaveFirma },
                            { "nit_consulta", nit }
                        };

                                var json = JsonSerializer.Serialize(data);
                                var contentNit = new StringContent(json, Encoding.UTF8, "application/json");
                                HttpResponseMessage responseNit = await httpClient.PostAsync(urlNit, contentNit);
                                string resultContentNit = await responseNit.Content.ReadAsStringAsync();
                                var resultadoJson = JObject.Parse(resultContentNit);
                                if (resultadoJson["nombre"].ToString() == "")
                                {
                                    return new ObjectResult(new { message = resultadoJson["mensaje"].ToString() });
                                }
                                else
                                {
                                    respuesta.nit = resultadoJson["nit"].ToString();
                                    respuesta.nombre = resultadoJson["nombre"].ToString();
                                    respuesta.respuesta = "1";
                                }
                            }
                        }


                        return respuesta;
                    }
                }
                else
                {
                    if (nit.ToUpper() == "CF")
                    {
                        RespuestaNitDpiInfileDTO respuestaCF = new RespuestaNitDpiInfileDTO();
                        respuestaCF.nit = "CF";
                        respuestaCF.nombre = "";
                        respuestaCF.respuesta = "1";
                        return respuestaCF;
                    }

                    ProveedoresidDTO? proveedores = new ProveedoresidDTO();
                    proveedores = await (from proveedor in context.Proveedores
                                      where proveedor.Nit == nit
                                      select new
                                      {
                                          nit = proveedor.Nit,
                                      }).Select(concepto => new ProveedoresidDTO
                                      {
                                          Nit = concepto.nit
                                      }).FirstOrDefaultAsync();
                    if (proveedores != null)
                    {
                        return new ObjectResult(new { message = "Este NIT ya se encuentra en uso" });
                    }
                    else
                    {
                        RespuestaNitDpiInfileDTO respuesta = new RespuestaNitDpiInfileDTO();
                        EmpresaDTO? empresas = new EmpresaDTO();
                        empresas = await (from empre in context.Empresas
                                          where empre.Codigo == entidad
                                          select new
                                          {
                                              alias = empre.Alias,

                                          }).Select(concepto => new EmpresaDTO
                                          {
                                              Alias = concepto.alias,
                                          }).FirstOrDefaultAsync();

                        SucursalDTO? sucursales = new SucursalDTO();
                        sucursales = await (from sucur in context.Sucursales
                                            where sucur.Codigo == sucursal
                                            select new
                                            {
                                                codigo = sucur.Codigo,
                                                llavesucursal = sucur.LlaveSucursal,
                                                llavefirma = sucur.LlaveFirma

                                            }).Select(concepto => new SucursalDTO
                                            {
                                                Codigo = concepto.codigo,
                                                LLaveSucursal = concepto.llavesucursal,
                                                LlaveFirma = concepto.llavefirma
                                            }).FirstOrDefaultAsync();

                        var url = "https://certificador.feel.com.gt/api/v2/servicios/externos/login";

                        var httpClient = new HttpClient();

                        var valores = new Dictionary<string, string>
                {
                    { "prefijo", empresas.Alias },
                    { "llave", sucursales.LlaveFirma }
                };

                        var content = new FormUrlEncodedContent(valores);

                        HttpResponseMessage response = await httpClient.PostAsync(url, content);

                        string resultContent = await response.Content.ReadAsStringAsync();
                        var loginResponse = JsonSerializer.Deserialize<RespuestaDatosClientesProveedores>(resultContent);
                        if (loginResponse == null)
                        {
                            return new ObjectResult(new { message = "Autenticación fallo con infile" });
                        }
                        else
                        {
                            if (loginResponse.resultado == false)
                            {
                                return new ObjectResult(new { message = loginResponse.descripcion });
                            }
                            else
                            {
                                string token = loginResponse.token;
                                var urlNit = "https://consultareceptores.feel.com.gt/rest/action";
                                var data = new Dictionary<string, string>
                        {
                            { "emisor_codigo", empresas.Alias },
                            { "emisor_clave", sucursales.LlaveFirma },
                            { "nit_consulta", nit }
                        };

                                var json = JsonSerializer.Serialize(data);
                                var contentNit = new StringContent(json, Encoding.UTF8, "application/json");
                                HttpResponseMessage responseNit = await httpClient.PostAsync(urlNit, contentNit);
                                string resultContentNit = await responseNit.Content.ReadAsStringAsync();
                                var resultadoJson = JObject.Parse(resultContentNit);
                                if (resultadoJson["nombre"].ToString() == "")
                                {
                                    return new ObjectResult(new { message = resultadoJson["mensaje"].ToString() });
                                }
                                else
                                {
                                    respuesta.nit = resultadoJson["nit"].ToString();
                                    respuesta.nombre = resultadoJson["nombre"].ToString();
                                    respuesta.respuesta = "1";
                                }
                            }
                        }
                        return respuesta;
                    }
                }

            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        public async Task<ActionResult<RespuestaNitDpiInfileDTO>> obtenerdatoscertificadordpi(string dpi, int entidad, int sucursal, int tipo)
        {
            try
            {
                string datosDPI = dpi.Length >= 4 ? dpi.Substring(dpi.Length - 4, 4) : dpi;

                Backend.Entidades.Municipio? municipio = await (from muni in context.Municipios
                                                                           where muni.CodigoDpi == datosDPI
                                                                           select new
                                                                           {
                                                                               departamento = muni.Departamento,
                                                                               municipio = muni.Codigo
                                                                           }).Select(concepto => new Backend.Entidades.Municipio
                                                                           {
                                                                               Departamento = concepto.departamento,
                                                                               Codigo = concepto.municipio
                                                                           }).FirstOrDefaultAsync();

                if (municipio == null)
                {
                    return new ObjectResult(new { message = "Este municipio no existe" });
                }

                if (tipo == 1)
                {
                    ClientesIdDTO? clientes = new ClientesIdDTO();
                    clientes = await (from cliente in context.Clientes
                                      where cliente.Dpi == dpi
                                      select new
                                      {
                                          nit = cliente.Nit,
                                      }).Select(concepto => new ClientesIdDTO
                                      {
                                          Nit = concepto.nit
                                      }).FirstOrDefaultAsync();
                    if (clientes != null)
                    {
                        return new ObjectResult(new { message = "Este DPI ya se encuentra en uso" });
                    }
                    else
                    {
                        RespuestaNitDpiInfileDTO respuesta = new RespuestaNitDpiInfileDTO();
                        EmpresaDTO? empresas = new EmpresaDTO();
                        empresas = await (from empre in context.Empresas
                                          where empre.Codigo == entidad
                                          select new
                                          {
                                              alias = empre.Alias,

                                          }).Select(concepto => new EmpresaDTO
                                          {
                                              Alias = concepto.alias,
                                          }).FirstOrDefaultAsync();

                        SucursalDTO? sucursales = new SucursalDTO();
                        sucursales = await (from sucur in context.Sucursales
                                            where sucur.Codigo == sucursal
                                            select new
                                            {
                                                codigo = sucur.Codigo,
                                                llavesucursal = sucur.LlaveSucursal,
                                                llavefirma = sucur.LlaveFirma

                                            }).Select(concepto => new SucursalDTO
                                            {
                                                Codigo = concepto.codigo,
                                                LLaveSucursal = concepto.llavesucursal,
                                                LlaveFirma = concepto.llavefirma
                                            }).FirstOrDefaultAsync();

                        var url = "https://certificador.feel.com.gt/api/v2/servicios/externos/login";

                        var httpClient = new HttpClient();

                        var valores = new Dictionary<string, string>
                {
                    { "prefijo", empresas.Alias },
                    { "llave", sucursales.LlaveFirma }
                };

                        var content = new FormUrlEncodedContent(valores);

                        HttpResponseMessage response = await httpClient.PostAsync(url, content);

                        string resultContent = await response.Content.ReadAsStringAsync();
                        var loginResponse = JsonSerializer.Deserialize<RespuestaDatosClientesProveedores>(resultContent);
                        if (loginResponse == null)
                        {
                            return new ObjectResult(new { message = "Autenticación fallo con infile" });
                        }
                        else
                        {
                            if (loginResponse.resultado == false)
                            {
                                return new ObjectResult(new { message = loginResponse.descripcion });
                            }
                            else
                            {
                                string token = loginResponse.token;
                                var urlNit = "https://certificador.feel.com.gt/api/v2/servicios/externos/cui";
                                var data = new Dictionary<string, string>
                        {
                            { "cui", dpi }
                        };

                                var json = JsonSerializer.Serialize(data);
                                var contentNit = new StringContent(json, Encoding.UTF8, "application/json");
                                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                                HttpResponseMessage responseNit = await httpClient.PostAsync(urlNit, contentNit);
                                string resultContentNit = await responseNit.Content.ReadAsStringAsync();
                                var resultadoJson = JObject.Parse(resultContentNit);
                                if (resultadoJson["descripcion"].ToString() != "OK")
                                {
                                    return new ObjectResult(new { message = resultadoJson["descripcion"].ToString() });
                                }
                                else
                                {
                                    var resultadoJsonDatos = JObject.Parse(resultadoJson["cui"].ToString());
                                    if (resultadoJsonDatos["fallecido"].ToString() == "NO")
                                    {
                                        respuesta.cui = resultadoJsonDatos["cui"].ToString();
                                        respuesta.nombre = resultadoJsonDatos["nombre"].ToString();
                                        respuesta.respuesta = "1";
                                        respuesta.departamento = municipio.Departamento.ToString();
                                        respuesta.municipio = municipio.Codigo.ToString();
                                    }
                                    else
                                    {
                                        return new ObjectResult(new { message = "La persona con el DPI digitado esta fallecida" });
                                    }
                                }
                            }
                        }



                        return respuesta;
                    }
                }
                else
                {
                    ProveedoresidDTO? proveedores = new ProveedoresidDTO();
                    proveedores = await (from proveedor in context.Proveedores
                                         where proveedor.Dpi == dpi
                                         select new
                                         {
                                             nit = proveedor.Nit,
                                         }).Select(concepto => new ProveedoresidDTO
                                         {
                                             Nit = concepto.nit
                                         }).FirstOrDefaultAsync();
                    if (proveedores != null)
                    {
                        return new ObjectResult(new { message = "Este DPI ya se encuentra en uso" });
                    }
                    else
                    {
                        RespuestaNitDpiInfileDTO respuesta = new RespuestaNitDpiInfileDTO();
                        EmpresaDTO? empresas = new EmpresaDTO();
                        empresas = await (from empre in context.Empresas
                                          where empre.Codigo == entidad
                                          select new
                                          {
                                              alias = empre.Alias,

                                          }).Select(concepto => new EmpresaDTO
                                          {
                                              Alias = concepto.alias,
                                          }).FirstOrDefaultAsync();

                        SucursalDTO? sucursales = new SucursalDTO();
                        sucursales = await (from sucur in context.Sucursales
                                            where sucur.Codigo == sucursal
                                            select new
                                            {
                                                codigo = sucur.Codigo,
                                                llavesucursal = sucur.LlaveSucursal,
                                                llavefirma = sucur.LlaveFirma

                                            }).Select(concepto => new SucursalDTO
                                            {
                                                Codigo = concepto.codigo,
                                                LLaveSucursal = concepto.llavesucursal,
                                                LlaveFirma = concepto.llavefirma
                                            }).FirstOrDefaultAsync();

                        var url = "https://certificador.feel.com.gt/api/v2/servicios/externos/login";

                        var httpClient = new HttpClient();

                        var valores = new Dictionary<string, string>
                {
                    { "prefijo", empresas.Alias },
                    { "llave", sucursales.LlaveFirma }
                };

                        var content = new FormUrlEncodedContent(valores);

                        HttpResponseMessage response = await httpClient.PostAsync(url, content);

                        string resultContent = await response.Content.ReadAsStringAsync();
                        var loginResponse = JsonSerializer.Deserialize<RespuestaDatosClientesProveedores>(resultContent);
                        if (loginResponse == null)
                        {
                            return new ObjectResult(new { message = "Autenticación fallo con infile" });
                        }
                        else
                        {
                            if (loginResponse.resultado == false)
                            {
                                return new ObjectResult(new { message = loginResponse.descripcion });
                            }
                            else
                            {
                                string token = loginResponse.token;
                                var urlNit = "https://certificador.feel.com.gt/api/v2/servicios/externos/cui";
                                var data = new Dictionary<string, string>
                        {
                            { "cui", dpi }
                        };

                                var json = JsonSerializer.Serialize(data);
                                var contentNit = new StringContent(json, Encoding.UTF8, "application/json");
                                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                                HttpResponseMessage responseNit = await httpClient.PostAsync(urlNit, contentNit);
                                string resultContentNit = await responseNit.Content.ReadAsStringAsync();
                                var resultadoJson = JObject.Parse(resultContentNit);
                                if (resultadoJson["descripcion"].ToString() != "OK")
                                {
                                    return new ObjectResult(new { message = resultadoJson["descripcion"].ToString() });
                                }
                                else
                                {
                                    var resultadoJsonDatos = JObject.Parse(resultadoJson["cui"].ToString());
                                    if (resultadoJsonDatos["fallecido"].ToString() == "NO")
                                    {
                                        respuesta.cui = resultadoJsonDatos["cui"].ToString();
                                        respuesta.nombre = resultadoJsonDatos["nombre"].ToString();
                                        respuesta.respuesta = "1";
                                        respuesta.departamento = municipio.Departamento.ToString();
                                        respuesta.municipio = municipio.Codigo.ToString();
                                    }
                                    else
                                    {
                                        return new ObjectResult(new { message = "La persona con el DPI digitado esta fallecida" });
                                    }
                                }
                            }
                        }



                        return respuesta;
                    }
                }
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        public async Task<ActionResult<RespuestaNitDpiInfileDTO>> ValidacioNitClienteProveedor(string nit, int tipo)
        {
            try
            {
                if (tipo == 1)
                {
                    if (nit.ToUpper() == "CF")
                    {
                        RespuestaNitDpiInfileDTO respuestaCF = new RespuestaNitDpiInfileDTO();
                        respuestaCF.nit = "CF";
                        respuestaCF.nombre = "";
                        respuestaCF.respuesta = "1";
                        return respuestaCF;
                    }

                    ClientesIdDTO? clientes = new ClientesIdDTO();
                    clientes = await (from cliente in context.Clientes
                                      where cliente.Nit == nit
                                      select new
                                      {
                                          nit = cliente.Nit,
                                      }).Select(concepto => new ClientesIdDTO
                                      {
                                          Nit = concepto.nit
                                      }).FirstOrDefaultAsync();
                    if (clientes != null)
                    {
                        return new ObjectResult(new { message = "Este NIT ya se encuentra en uso" });
                    }


                    RespuestaNitDpiInfileDTO respuesta = new RespuestaNitDpiInfileDTO();
                    respuesta.nit = nit;
                    respuesta.nombre = "";
                    respuesta.respuesta = "1";
                    return respuesta;

                } else
                {

                    if (nit.ToUpper() == "CF")
                    {
                        RespuestaNitDpiInfileDTO respuestaCF = new RespuestaNitDpiInfileDTO();
                        respuestaCF.nit = "CF";
                        respuestaCF.nombre = "";
                        respuestaCF.respuesta = "1";
                        return respuestaCF;
                    }

                    ProveedoresidDTO? Proveedores = new ProveedoresidDTO();
                    Proveedores = await (from proveedor in context.Proveedores
                                      where Proveedores.Nit == nit
                                      select new
                                      {
                                          nit = proveedor.Nit,
                                      }).Select(concepto => new ProveedoresidDTO
                                      {
                                          Nit = concepto.nit
                                      }).FirstOrDefaultAsync();
                    if (Proveedores != null)
                    {
                        return new ObjectResult(new { message = "Este NIT ya se encuentra en uso" });
                    }


                    RespuestaNitDpiInfileDTO respuesta = new RespuestaNitDpiInfileDTO();
                    respuesta.nit = nit;
                    respuesta.nombre = "";
                    respuesta.respuesta = "1";
                    return respuesta;
                }
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        public async Task<ActionResult<RespuestaNitDpiInfileDTO>> validacionDpiClienteProveedor(string dpi, int tipo)
        {
            try
            {
                if (tipo == 1)
                {
                    ClientesIdDTO? clientes = new ClientesIdDTO();
                    clientes = await (from cliente in context.Clientes
                                      where cliente.Dpi == dpi
                                      select new
                                      {
                                          dpi = cliente.Dpi,
                                      }).Select(concepto => new ClientesIdDTO
                                      {
                                          Nit = concepto.dpi
                                      }).FirstOrDefaultAsync();
                    if (clientes != null)
                    {
                        return new ObjectResult(new { message = "Este DPI ya se encuentra en uso" });
                    }
                    RespuestaNitDpiInfileDTO respuesta = new RespuestaNitDpiInfileDTO();
                    respuesta.cui = dpi;
                    respuesta.nombre = "";
                    respuesta.respuesta = "1";
                    return respuesta;
                }
                else
                {
                    ProveedoresidDTO? proveedores = new ProveedoresidDTO();
                    proveedores = await (from proveedor in context.Proveedores
                                         where proveedor.Dpi == dpi
                                      select new
                                      {
                                          dpi = proveedor.Dpi,
                                      }).Select(concepto => new ProveedoresidDTO
                                      {
                                          Nit = concepto.dpi
                                      }).FirstOrDefaultAsync();
                    if (proveedores != null)
                    {
                        return new ObjectResult(new { message = "Este DPI ya se encuentra en uso" });
                    }
                    RespuestaNitDpiInfileDTO respuesta = new RespuestaNitDpiInfileDTO();
                    respuesta.cui = dpi;
                    respuesta.nombre = "";
                    respuesta.respuesta = "1";
                    return respuesta;
                }
            }

            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        public async Task<ActionResult<List<SelectFormulario>>> selectcombustible()
        {
            try
            {
                List<SelectFormulario> lista = await (from combustible in context.Combustibles
                                                      where combustible.Estado == true
                                                      select new
                                                      {
                                                          Codigo = combustible.Codigo,
                                                          Descripcion = combustible.Nombre
                                                      }).Select(concepto => new SelectFormulario
                                                      {
                                                          codigo = Convert.ToInt32(concepto.Codigo),
                                                          descripcion = concepto.Descripcion,
                                                      }).ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        public async Task<ActionResult<Combustible>> obtenerdatoscombustible(int codigo)
        {
            try
            {
                Combustible lista = new Combustible();

                lista = await context.Combustibles.FirstOrDefaultAsync(x => x.Codigo == codigo);
                if (lista == null)
                {
                    return new ObjectResult(new { message = "Este combustible no existe" });
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


