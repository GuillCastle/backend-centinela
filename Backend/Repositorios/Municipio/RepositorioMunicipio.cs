using AutoMapper;
using Backend.DTOs.Municipio;
using Backend.Repositorios.Municipio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositorios.Municipio
{
    public class RepositorioMunicipio : IRepositorioMunicipio
    {
        private readonly ILogger<RepositorioMunicipio> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RepositorioMunicipio(ILogger<RepositorioMunicipio> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ActionResult<List<MunicipioDTO>>> get()
        {
            try
            {
                List<MunicipioDTO> lista = await (from Municipio in context.Municipios

                                                 select new
                                                 {
                                                     Codigo = Municipio.Codigo,
                                                     Descripcion = Municipio.Descripcion,
                                                     Departamento = Municipio.Departamento,
                                                     CodigoPostal = Municipio.CodigoPostal,
                                                 }).Select(concepto => new MunicipioDTO
                                                 {
                                                     Codigo = concepto.Codigo,
                                                     Descripcion = concepto.Descripcion,
                                                     Departamento = concepto.Departamento,
                                                     CodigoPostal = concepto.CodigoPostal,
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
