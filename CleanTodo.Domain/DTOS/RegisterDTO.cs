using CleanTodo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTodo.Domain.DTOS
{
    public class RegisterDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public RegisterDTO() { }

        public RegisterDTO(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Password = user.Password;
        }
    }
}
