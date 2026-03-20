using Backend.ApiBehavior;
using Backend.Datos;
using Backend.Filtros;
using Backend.Repositorios.Departamento;
using Backend.Repositorios.Login;
using Backend.Repositorios.Municipio;

using Backend.Repositorios.Roles;
using Backend.Repositorios.Usuarios;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using Backend.Repositorios.Shared;

using Backend.Repositorios.Reportes.ReportesApertura;
using Backend.Repositorios.EmpresaSucursal;



namespace Backend
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {


            //services.AddControllers().AddJsonOptions(x =>
            //x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            services.AddHttpClient();
            services.AddScoped<IRepositorioRol, RepositorioRol>();
            services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
            services.AddScoped<IRepositorioLogin, RepositorioLogin>();
            services.AddScoped<IRepositorioDepartamento, RepositorioDepartamento>();
            services.AddScoped<IRepositorioMunicipio, RepositorioMunicipio>();
            services.AddScoped<IRepositorioShared, RepositorioShared>();
            services.AddScoped<IRepositorioReportesApertura, RepositorioReportesApertura>();
            services.AddScoped<IRepositorioEmpresaSucursal, RepositorioEmpresaSucursal>();
            services.AddAutoMapper(typeof(Startup));
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configRoot.GetConnectionString("defaultConnection")));
            var cadenaConexionSqlConfiguracion = new AccesoDatos(configRoot.GetConnectionString("defaultConnection"));
            services.AddSingleton(cadenaConexionSqlConfiguracion);


            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configRoot.GetValue<string>("llavejwt"))),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            //services.AddResponseCaching();
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(FiltroDeExcepcion));
                options.Filters.Add(typeof(ParsearBadRequests));
            }).ConfigureApiBehaviorOptions(BehaviorBadRequests.Parsear);

            var allowedOrigins = configRoot
            .GetSection("Cors:AllowedOrigins")
            .Get<string[]>() ?? [];


            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API", Version = "v1" });
            });

            //var PostgreSQLConnectionConfiguration = new PostgreSQLConfiguration(configRoot.GetConnectionString("PostgreSQL"));
            //services.AddSingleton(PostgreSQLConnectionConfiguration);
            //services.AddScoped<IRepositorioPrueba, RepositorioPrueba>();


        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
           

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            //app.UseFileServer(new FileServerOptions
            //{
            //    FileProvider = new PhysicalFileProvider(@"D:\imagenesCastillo"),
            //    RequestPath = "/imagenesCastillo",  // Establece la ruta base en la URL para las imágenes
            //    EnableDirectoryBrowsing = false
            //});

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                Path.Combine(@"E:\imagenesCastillo")),
                RequestPath = "/uploads"
            });


            app.UseRouting();

            app.UseCors();
       

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.Run();
        }
    }
}