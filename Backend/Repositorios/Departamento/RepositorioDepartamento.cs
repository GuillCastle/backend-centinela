using AutoMapper;
using Backend.DTOs.Departamento;
using Backend.DTOs.Producto;
using Backend.DTOs.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositorios.Departamento
{
    public class RepositorioDepartamento : IRepositorioDepartamento
    {
        private readonly ILogger<RepositorioDepartamento> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public RepositorioDepartamento(ILogger<RepositorioDepartamento> logger, ApplicationDbContext context, IMapper mapper)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ActionResult<List<DepartamentoDTO>>> get()
        {
            try
            {
                List<DepartamentoDTO> lista = await (from departamento in context.Departamentos

                                                 select new
                                                 {
                                                     Codigo = departamento.Codigo,
                                                     Descripcion = departamento.Descripcion,
                                                     Pais = departamento.Pais, 
                                                 }).Select(concepto => new DepartamentoDTO
                                                 {
                                                     Codigo = concepto.Codigo,
                                                     Descripcion = concepto.Descripcion,
                                                     Pais = concepto.Pais,
                                                 }).ToListAsync();

                return lista;
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        public async Task<ActionResult<DepartamentoIdDTO>> getid(int codigo)
        {
            try
            {
                DepartamentoIdDTO? departamentoid = new DepartamentoIdDTO();

                departamentoid = await (from departamento in context.Departamentos
                                    where departamento.Codigo == codigo
                                    select new
                                    {
                                        Codigo = departamento.Codigo,
                                        Descripcion = departamento.Descripcion,
                                        Pais = departamento.Pais,
                                    }).Select(concepto => new DepartamentoIdDTO
                                    {
                                        Codigo = concepto.Codigo,
                                        Descripcion = concepto.Descripcion,
                                        Pais = concepto.Pais,
                                    }).FirstOrDefaultAsync();

                if (departamentoid == null)
                {
                    return new ObjectResult(new { message = "No se encontro el registro" });
                }

                return departamentoid;
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        public async Task<ActionResult<List<SelectFormulario>>> obtenerDepartamento()
        {
            try
            {
                List<SelectFormulario> lista = await (from departamento in context.Departamentos
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
    }
}
