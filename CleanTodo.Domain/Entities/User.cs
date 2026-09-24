namespace CleanTodo.Domain.Entities;

public class User
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }

    public User( string userName,string passWord)
    {
        Id = Guid.NewGuid();
        Username= userName;
        Password = passWord;
    }
    public User()
    {
    }

}