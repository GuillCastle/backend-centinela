using AutoMapper;
using Backend.DTOs.Utils;
using Backend.Repositorios.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositorios.TipoEvento
{
    public class RepositorioTipoEvento : IRepositorioTipoEvento
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RepositorioTipoEvento> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RepositorioTipoEvento(ILogger<RepositorioTipoEvento> logger, ApplicationDbContext context, IMapper mapper, IHttpClientFactory httpClientFactory)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            _httpClient = httpClientFactory.CreateClient();
        }

        public async Task<ActionResult<List<SelectFormulario>>> selecttipoevento()
        {
            try
            {
                List<SelectFormulario> lista = await (from tipo in context.TipoEventos
                                                      where tipo.Estado == 1
                                                      select new
                                                      {
                                                          Codigo = tipo.Codigo,
                                                          Descripcion = tipo.Descripcion
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
