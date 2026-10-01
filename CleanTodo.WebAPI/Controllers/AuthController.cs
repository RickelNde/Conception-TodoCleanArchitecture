using CleanTodo.Application.UseCase;
using CleanTodo.Application.UseCases.User;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Infrastructure.Services;
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
    public class AuthController(LoginUseCase loginUseCase, RegisterUseCase registerUseCase, JwtService _jwtService) :ControllerBase
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
        public async Task<IActionResult> Login([FromBody] LoginDTO login)
        {
            try
            {
                var user = await loginUseCase.Execute(login);

                var token = _jwtService.GenerateToken(
                    user.Id.ToString(),
                    user.Username
                );

                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Lax,
                    Expires = DateTimeOffset.UtcNow.AddHours(1)
                };

                Response.Cookies.Append("AuthToken", token, cookieOptions);

                return Ok(new
                {
                    message = "Connexion réussie",
                    username = user.Username
                });
            }
            catch (Exception)
            {
                return Unauthorized(new
                {
                    message = "Nom d'utilisateur ou mot de passe incorrect."
                });
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            
            Response.Cookies.Delete("AuthToken", new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Lax
            });

            return Ok(new { message = "Déconnexion réussie" });
        }


    }
}
