namespace CleanTodo.Domain.Entities;

public class Ship
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Captain { get; set; }
    public int CrewSize { get; set; }
    public int GoldCargo { get; set; }
    public string Status { get; set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastModified { get; set; }

    public Ship(string name, string captain, int crewSize, int goldCargo, string createdBy)
    {
        Id = Guid.NewGuid();
        Name = name;
        Captain = captain;
        CrewSize = crewSize;

        GoldCargo = goldCargo;
        Status = "Docked";
        CreatedBy = createdBy;
        CreatedAt = DateTime.UtcNow;
        LastModified = DateTime.UtcNow;
    }

    public Ship() { }
}