using CleanTodo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTodo.Domain.DTOS
{
    public class UpdateShipDto
    {
        public string Name { get; set; }
        public string Captain { get; set; } 
        public int CrewSize { get; set; }
        public int GoldCargo { get; set; }


        public UpdateShipDto() { }

        public UpdateShipDto(Ship ship)
        {
            Name = ship.Name;
            Captain = ship.Captain;
            CrewSize = ship.CrewSize;
            GoldCargo = ship.GoldCargo;
           
        }
    }
}
