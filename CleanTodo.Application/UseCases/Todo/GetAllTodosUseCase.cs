using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCase;

public class GetAllUsersUseCase
{
    private readonly ITodoRepository _todoRepository;

    public GetAllUsersUseCase(ITodoRepository todoRepository)
    {
        _todoRepository = todoRepository;
    }

    public async Task<IList<TodoDto>> Execute()
    {
        var todos = await _todoRepository.GetAll();
        return todos.Select(x => new TodoDto(x)).ToList();
    }
}