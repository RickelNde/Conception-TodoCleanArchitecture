using CleanTodo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTodo.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetAll();
        Task<User?> FindById(Guid id);
        Task<User> AddUser(User user);
    }
}
