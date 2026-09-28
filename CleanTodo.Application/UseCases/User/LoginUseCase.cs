using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTodo.Application.UseCases.User
{
    public class LoginUseCase
    {
        private readonly IUserRepository _userRepository;

        public LoginUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Domain.Entities.User> Execute(LoginDTO login)
        {
            var user = await _userRepository.GetByUsernameAsync(login.Username);
            Console.WriteLine($"Utilisateur trouvé : {user?.Username ?? "AUCUN"}");

            if (user is null)
            {
                throw new NotFoundException("Utilisateur non trouvé.");
            }

            bool passwordValid = BCrypt.Net.BCrypt.Verify(login.Password, user.Password);

            if (!passwordValid)
            {
                throw new NotFoundException("Nom d'utilisateur ou mot de passe incorrect.");
            }

            return user;
        }
    }
}