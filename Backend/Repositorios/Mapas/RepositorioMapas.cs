using AutoMapper;
using Backend.DTOs.Evento;
using Backend.DTOs.Mapas;
using Backend.Entidades;
using Backend.Hubs;
using Backend.Repositorios.Evento;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Data;

namespace Backend.Repositorios.Mapas
{
    public class RepositorioMapas : IRepositorioMapas
    {
        private readonly ILogger<RepositorioEvento> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;


        public RepositorioMapas(ILogger<RepositorioEvento> logger, ApplicationDbContext context, IMapper mapper, IConfiguration configuration, IWebHostEnvironment env)
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
            this.configuration = configuration;
            this.env = env;
        }

        public async Task<ActionResult<List<DepartamentoMapaDTO>>> obtenerMapaDepartamento()
        {
            try
            {
                var departamentos = new List<DepartamentoMapaDTO>();

                using var connection = context.Database.GetDbConnection();

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = "sp_obtener_mapa_departamento";
                command.CommandType = CommandType.StoredProcedure;

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    departamentos.Add(new DepartamentoMapaDTO
                    {
                        Codigo = reader["Codigo"] != DBNull.Value
                            ? Convert.ToInt32(reader["Codigo"])
                            : 0,

                        Descripcion = reader["Descripcion"] != DBNull.Value
                            ? reader["Descripcion"].ToString() ?? string.Empty
                            : string.Empty,

                        Wkt = reader["Wkt"] != DBNull.Value
                            ? reader["Wkt"].ToString()
                            : null
                    });
                }

                return departamentos;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener mapa de departamentos");
                return new ObjectResult(new
                {
                    mensaje = ex.Message
                })
                {
                    StatusCode = 500
                };
            }
        }

        public async Task<ActionResult<List<EventoMapaDTO>>> obtenerMapaEventos(int codigo)
        {
            try
            {
                var evento = new List<EventoMapaDTO>();

                using var connection = context.Database.GetDbConnection();

                if (connection.State != ConnectionState.Open)
                    await connection.OpenAsync();

                using var command = connection.CreateCommand();
                command.CommandText = "sp_obtener_mapa_evento";
                command.CommandType = CommandType.StoredProcedure;

                // 👉 PARAMETRO
                var parametro = command.CreateParameter();
                parametro.ParameterName = "@codigo"; // importante que coincida con el SP
                parametro.Value = codigo;
                parametro.DbType = DbType.Int32;

                command.Parameters.Add(parametro);

                using var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    evento.Add(new EventoMapaDTO
                    {
                        Codigo = reader["Codigo"] != DBNull.Value
                            ? Convert.ToInt32(reader["Codigo"])
                            : 0,

                        Descripcion = reader["Descripcion"] != DBNull.Value
                            ? reader["Descripcion"].ToString() ?? string.Empty
                            : string.Empty,

                        Wkt = reader["Wkt"] != DBNull.Value
                            ? reader["Wkt"].ToString()
                            : null,
                        Estado = reader["Estado"] != DBNull.Value
                            ? Convert.ToInt32(reader["Estado"])
                            : 0,
                    });
                }

                return evento;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error al obtener mapa de eventos");
                return new ObjectResult(new
                {
                    mensaje = ex.Message
                })
                {
                    StatusCode = 500
                };
            }
        }

    }
}
