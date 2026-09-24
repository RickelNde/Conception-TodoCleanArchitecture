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
    }
}
