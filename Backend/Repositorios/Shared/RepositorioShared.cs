using AutoMapper;
using Backend.DTOs;
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

        

        

        
        

        

        

    }
}


