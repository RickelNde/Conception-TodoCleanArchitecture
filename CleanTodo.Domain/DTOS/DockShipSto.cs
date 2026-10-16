namespace CleanTodo.Domain.DTOS
{
    public class DockShipDto
    {
        public string Name { get; set; }
        public string Captain { get; set; } 
        public int CrewSize { get; set; }
        public int GoldCargo { get; set; }
        public string Status { get; set; } 
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
    }
}