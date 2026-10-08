using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTodo.Application.UseCases
{
    public class GetShipUseCase
    {
        private readonly IShipRepository _shipRepository;

        public GetShipUseCase(IShipRepository shipRepository)
        {
            _shipRepository = shipRepository;
        }

        public async Task<ShipDto> Execute(Guid id)
        {
            Ship? ship = await _shipRepository.FindById(id);
            if (ship == null)
            {
                throw new NotFoundException(id);
            }
            return new ShipDto(ship);
        }
    }
}
