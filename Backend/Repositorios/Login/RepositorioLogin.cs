using AutoMapper;
using Backend.DTOs.Login;
using Backend.DTOs.Marca;
using Backend.DTOs.Rol;
using Backend.DTOs.Usuario;
using Backend.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Backend.Repositorios.Login
{
    public class RepositorioLogin : IRepositorioLogin
    {
        private readonly ApplicationDbContext context;
        private readonly IConfiguration configuration;

        public RepositorioLogin(ApplicationDbContext context, IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }

        public async Task<ActionResult<RespuestaAutenticacion>> login(CredencialesUsuario credenciales)
        {
            try
            {
                Usuario usuario = new Usuario();

                usuario = await context.Usuarios.FirstOrDefaultAsync(x => x.NombreUsuario == credenciales.Usuario);

                if(usuario == null)
                {
                    return new RespuestaAutenticacion()
                    {
                        verificador = "error en el login",
                        api_token = "",
                        refreshToken = DateTime.Now,
                    };
                }

                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] buff = new byte[6 - 1];
                rng.GetBytes(buff);
                string salt = Convert.ToBase64String(buff);

                string saltAndPwd = string.Concat(credenciales.Password, usuario.Salt);
                string hashedPwd = "";
                using (SHA1 sha1 = SHA1.Create())
                {
                    byte[] hashBytes = sha1.ComputeHash(Encoding.UTF8.GetBytes(saltAndPwd));
                    hashedPwd = Convert.ToBase64String(hashBytes);
                }
                Usuario usuariovalidacion = new Usuario();
                usuariovalidacion = await context.Usuarios.FirstOrDefaultAsync(x => x.NombreUsuario == credenciales.Usuario && x.Clave == hashedPwd);
                if(usuariovalidacion == null)
                {
                    return new RespuestaAutenticacion()
                    {
                        verificador = "Usuario o contraseña incorrecto, verifique su información o comuniquese con el administrador",
                        api_token = "",
                        refreshToken = DateTime.Now,
                    };
                }

                Usuario usuariovalidacionestado = new Usuario();
                usuariovalidacionestado = await context.Usuarios.FirstOrDefaultAsync(x => x.NombreUsuario == credenciales.Usuario && x.Clave == hashedPwd && x.Estado == 1);
                if (usuariovalidacionestado == null)
                {
                    return new RespuestaAutenticacion()
                    {
                        verificador = "Usuario o contraseña incorrecto, verifique su información o comuniquese con el administrador",
                        api_token = "",
                        refreshToken = DateTime.Now,
                    };
                }

                RespuestaAutenticacion respuestaAutenticacion = new RespuestaAutenticacion();
                respuestaAutenticacion = await ContruirToken(credenciales);
                return respuestaAutenticacion;
            }
            catch (Exception ex)
            {
                return new ObjectResult(new { message = ex.Message.ToString() });
            }
        }

        

        private async Task<RespuestaAutenticacion> ContruirToken(CredencialesUsuario credenciales)
        {
            var claims = new List<Claim>()
            {
                new Claim("usuario", credenciales.Usuario)
            };

            var llave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["llavejwt"]));
            var creds = new SigningCredentials(llave, SecurityAlgorithms.HmacSha256);

            var expiracion = DateTime.UtcNow.AddDays(1);

            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiracion, signingCredentials: creds);

            return new RespuestaAutenticacion()
            {
                verificador = "1",
                api_token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = expiracion
            };  

        }

        


    }
}
