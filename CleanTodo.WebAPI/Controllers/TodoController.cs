using CleanTodo.Application.UseCase;
using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TodoController(GetAllTodosUseCase getAllUseCase, GetTodoUseCase getTodoUseCase) : ControllerBase
{
    private GetAllTodosUseCase _getAllUseCase = getAllUseCase;
    private GetTodoUseCase _getTodoUseCase = getTodoUseCase;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoDto>>> GetAll()
    {
        var todos = await _getAllUseCase.Execute();
        return Ok(todos);
    }

    //Cadeau! pour le create. On utilise un CreatedAtAction qui retourne un code http 201 et un header location avec l'url du nouvel élément créé.
    //
    [HttpPost]
    //public async Task<ActionResult<TodoDto>> Create([FromBody] CreateTodoDto createTodoDto)
    //{
    //    TodoDto todo = await _createUseCase.Execute(createTodoDto);

    //    return CreatedAtAction(
    //        nameof(Get),
    //        new { id = todo.Id },
    //        todo);
    //}

    [HttpGet("{id}")] // /api/todo/ton_id
    public async Task<IActionResult> Get(Guid id)
    {
        try
        {
            TodoDto todo = await _getTodoUseCase.Execute(id);
            return Ok(todo);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    // Pour le delete et le update, tu peux retourn un noContent (http 204) qui dit :"Ça fonctionné, je n'ai rien à te retourner"
    //return NoContent();
}
