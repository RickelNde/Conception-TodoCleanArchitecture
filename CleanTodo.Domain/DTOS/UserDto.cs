using CleanTodo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanTodo.Domain.DTOS
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public UserDto(User user)
        {

            Id = user.Id;
            Username = user.Username;
            Password = user.Password;
        }
        public UserDto()
        {
            
        }
    }
}
