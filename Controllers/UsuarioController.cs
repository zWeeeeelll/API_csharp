using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API_csharp.Filters;
using API_csharp.Infraestruture.Data;
using API_csharp.Models;
using API_csharp.Models.Usuarios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.Data;
using Swashbuckle.AspNetCore.Annotations;

namespace API_csharp.Controllers
{ 
    [Route("api/v1/usuario")]
    [ApiController]
    
    public class UsuarioController : ControllerBase
    {
        /// <summary>
        /// Este serviço permite autenticar um usário cadastrado e ativo.
        /// </summary>
        /// <param name="loginViewModelInput"></param>
        /// <returns>Retornna status ok, dados do usuario e o token em caso</returns>
        [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
        [SwaggerResponse(statusCode: 400, description: "Campos obrigtórios", Type = typeof(ValidaCampoViewModelOutput))]
        [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
        [ValidacaoModelStateCustomizado]
        [HttpPost]
        [Route("logar")]
        public IActionResult Logar(LoginViewModelInput loginViewModelInput)
        {
            var usuarioViewModelOutput = new UsuarioViewModelOutput()
            {
                Codigo = 1,
                Login = "isaac",
                Email = "isaac@teste.com"
            };

            var secret = Encoding.ASCII.GetBytes("Mzfsdfl;sdflskdlfll;23e2ds");
            var symmetricSecurityKey = new SymmetricSecurityKey(secret);
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOutput.Codigo.ToString()),
                    new Claim(ClaimTypes.Name, usuarioViewModelOutput.Login.ToString()),
                    new Claim(ClaimTypes.Email, usuarioViewModelOutput.Email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);
            return Ok(new
            {
                Token = token,
                Usuario = usuarioViewModelOutput
            });
        }

        
        [HttpPost]
        [ValidacaoModelStateCustomizado]
        [Route("registrar")]
        public IActionResult Registrar(RegistrarViewModelInput registrarViewModelInput)
        {
            var options = new DbContextOptionsBuilder<CursoDbContext>();
            options.UseSqlServer("");
            CursoDbContext contexto = new CursoDbContext(options);
            return Created("", registrarViewModelInput);
        }
    }
}