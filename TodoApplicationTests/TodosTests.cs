
//using CleanTodo.Application.UseCase;
//using CleanTodo.Application.Validators;
//using CleanTodo.Domain.Entities;
//using CleanTodo.Domain.Exceptions;
//using CleanTodo.Domain.Interfaces.Repositories;
//using CleanTodo.Domain.DTOS;
//using FluentValidation;
//using Moq;

//namespace TodoApplicationTests;

//public class TodosTests
//{
//    private Mock<ITodoRepository> _todoRepositoryMock;
//    private CreateTodoUseCase _createTodoUseCase;
//    private GetTodoUseCase _getTodoUseCase;
//    private DeleteTodoUseCase _deleteTodoUseCase;
//    private GetAllTodosUseCase _getAllTodosUseCase;
//    private ToggleTodoCompleteStatusUseCase _toggleTodoCompleteStatusUseCase;
//    private IValidator<CreateTodoDto> _createTodoValidator;
//    Todo todo1 = new Todo { Id = Guid.NewGuid(), Text = "Test Todo 1" };
//    Todo todo2 = new Todo { Id = Guid.NewGuid(), Text = "Test Todo 2" };


//    [SetUp]
//    public void Setup()
//    {
//        _createTodoValidator = new CreateTodoValidation();
//        _todoRepositoryMock = new Mock<ITodoRepository>();
//        _createTodoUseCase = new CreateTodoUseCase(_todoRepositoryMock.Object, _createTodoValidator);
//        _getTodoUseCase = new GetTodoUseCase(_todoRepositoryMock.Object);
//        _deleteTodoUseCase = new DeleteTodoUseCase(_todoRepositoryMock.Object);
//        _getAllTodosUseCase = new GetAllTodosUseCase(_todoRepositoryMock.Object);
//        _toggleTodoCompleteStatusUseCase = new ToggleTodoCompleteStatusUseCase(_todoRepositoryMock.Object);

//        // Arrange
//        _todoRepositoryMock.Setup(repo => repo.Add(It.IsAny<Todo>())).ReturnsAsync(todo1);
//        _todoRepositoryMock.Setup(repo => repo.Delete(It.IsAny<Guid>()));
//        _todoRepositoryMock.Setup(repo => repo.ToggleCompleteStatus(It.IsAny<Guid>()));
//        _todoRepositoryMock.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Todo> { todo1, todo2 });
//        _todoRepositoryMock.Setup(repo => repo.FindById(It.Is<Guid>(id => id == todo1.Id))).ReturnsAsync(todo1);
//    }

//    [Test]
//    public async Task CreateTodo_ShouldReturnCreatedTodo()
//    {
//        // Arrange
//        CreateTodoDto createTodoDto = new CreateTodoDto { Title = "Test Todo" };
//        // Act
//        var result = await _createTodoUseCase.Execute(createTodoDto);

//        // Assert
//        Assert.That(todo1.Id == result.Id, "Todo is returned");
//        Assert.That(todo1.Text == result.Title, "Same text");
//    }

//    [Test]
//    public async Task GetTodo_ShouldReturnTodo()
//    {
//        // Arrange
//        CreateTodoDto createTodoDto = new CreateTodoDto { Title = "Test Todo" };
//        // Act
//        var result = await _getTodoUseCase.Execute(todo1.Id);

//        // Assert
//        Assert.That(todo1.Id == result.Id, "Todo is returned");
//        Assert.That(todo1.Text == result.Title, "Same text");
//    }

//    [Test]
//    public async Task DeleteTodo_ShouldCallRepositoryDelete()
//    {
//        // Act
//        await _deleteTodoUseCase.Execute(todo1.Id);

//        // Assert
//        _todoRepositoryMock.Verify(repo => repo.Delete(todo1.Id), Times.Once);
//    }

//    [Test]
//    public async Task GetAllTodos_ShouldReturnAllTodos()
//    {
//        // Act
//        var result = await _getAllTodosUseCase.Execute();

//        // Assert
//        Assert.That(result.Count == 2, "Got 2 todos");
//        Assert.That(todo1.Id == result[0].Id, "Both todos are returned");
//        Assert.That(todo2.Id == result[1].Id, "Both todos are returned");
//    }

//    [Test]
//    public async Task ToggleTodoCompleteStatus_ShouldCallRepositoryToggleCompleteStatus()
//    {
//        // Act
//        await _toggleTodoCompleteStatusUseCase.Execute(todo1.Id);

//        // Assert
//        _todoRepositoryMock.Verify(repo => repo.ToggleCompleteStatus(todo1.Id), Times.Once);
//    }

//    [Test]
//    public async Task DelitingAMissingTodo_ShouldThrowAnError()
//    {
//        Guid idOfAFakeTodo = Guid.NewGuid();
//        // Act

//        // Assert
//        Assert.ThrowsAsync<NotFoundException>(async () => await _toggleTodoCompleteStatusUseCase.Execute(idOfAFakeTodo));
//        _todoRepositoryMock.Verify(repo => repo.FindById(idOfAFakeTodo), Times.Once);
//    }
//}