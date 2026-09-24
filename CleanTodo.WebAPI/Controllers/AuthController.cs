using CleanTodo.Application.UseCase;
using CleanTodo.Application.UseCases.User;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CleanTodo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(LoginUseCase loginUseCase, RegisterUseCase registerUseCase) :ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO register)
        {
          
                var user = await registerUseCase.Execute(register);

                return Ok(new
                {
                    message = "Utilisateur enregistré avec succès.",
                    userId = user.Id,
                    username = user.Username
                });
            
           
        }
        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO login)
        {
            if (login.Username == "admin" && login.Password == "password")
            {
                var claims = new[]
                {
            new Claim(ClaimTypes.Name, login.Username),
            new Claim(ClaimTypes.Role, "Admin")
        };

                var key = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes("YourSuperSecretKey123888888888888ssssssss")
                );

                var creds = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256
                );

                var token = new JwtSecurityToken(
                    issuer: "your-app",
                    audience: "your-app",
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1),
                    signingCredentials: creds
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }

            return Unauthorized(new
            {
                message = "Nom d'utilisateur ou mot de passe incorrect."
            });
        }
    }
}
