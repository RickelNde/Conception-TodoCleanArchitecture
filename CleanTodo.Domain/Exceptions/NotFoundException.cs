namespace CleanTodo.Domain.Exceptions;

public class NotFoundException : Exception
{
    public Guid Id { get; set; }

    public NotFoundException(Guid id)
    {
        Id = id;
    }
    public NotFoundException(string message) : base(message)
    {
    }
}
