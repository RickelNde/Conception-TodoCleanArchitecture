using CleanTodo.Domain.Entities;

namespace CleanTodo.Domain.DTOS
{
    public class CreateShipDto
    {
        public string Name { get; set; }
        public string Captain { get; set; }
        public int CrewSize { get; set; }
        public int GoldCargo { get; set; }

        public CreateShipDto() { }

        public CreateShipDto(Ship ship)
        {
            Name = ship.Name;
            Captain = ship.Captain;
            CrewSize = ship.CrewSize;
            GoldCargo = ship.GoldCargo;
        }
    }
}