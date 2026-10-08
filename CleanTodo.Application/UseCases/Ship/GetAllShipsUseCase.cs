using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTodo.Application.UseCases
{
    public class GetAllShipsUseCase
    {
        private readonly IShipRepository _shipRepository;

        public GetAllShipsUseCase(IShipRepository shipRepository)
        {
            _shipRepository = shipRepository;
        }

        public async Task<IList<ShipDto>> Execute()
        {
            var ships = await _shipRepository.GetAll();
            return ships.Select(x => new ShipDto(x)).ToList();
        }
    }
}
