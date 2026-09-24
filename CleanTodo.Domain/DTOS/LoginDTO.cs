using CleanTodo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTodo.Domain.DTOS
{
    public class LoginDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

      public LoginDTO() { }
       public LoginDTO(User user)
        {
            Id = user.Id;
            Username = user.Username;
            Password = user.Password;
        }
    }



}
