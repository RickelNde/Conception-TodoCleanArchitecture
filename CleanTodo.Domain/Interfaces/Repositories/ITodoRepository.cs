using CleanTodo.Domain.Entities;

namespace CleanTodo.Domain.Interfaces.Repositories;

public interface ITodoRepository
{
    Task<List<Todo>> GetAll();
    Task<Todo?> FindById(Guid id);
    Task<Todo> AddTodo(Todo todo);
    Task<Todo> UpdateTodo(Todo todo);
    Task DeleteTodo(Guid id);
}
