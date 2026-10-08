using CleanTodo.Domain.Entities;

namespace CleanTodo.Domain.DTOS;

public class ShipDto
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

    public ShipDto() { }

   
    public ShipDto(Ship ship)
    {
        Id = ship.Id;
        Name = ship.Name;
        Captain = ship.Captain;
        CrewSize = ship.CrewSize;
        GoldCargo = ship.GoldCargo;
        Status = ship.Status;
        CreatedBy = ship.CreatedBy;
        CreatedAt = ship.CreatedAt;
        LastModified = ship.LastModified;
    }
}