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

                // Générer le token
                var token = _jwtService.GenerateToken(
                    user.Id.ToString(),
                    user.Username
                );

                // Retourner le token
                return Ok(new
                {
                    message = "Connexion réussie.",
                    token = token
                });
            }
            catch (NotFoundException)
            {
                return Unauthorized(new
                {
                    message = "Nom d'utilisateur ou mot de passe incorrect."
                });
            }
        }


    }
}
