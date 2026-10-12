using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTodo.Application.UseCases
{
    public class DeleteShipUseCase
    {
        private readonly IShipRepository _shipRepository;

        public DeleteShipUseCase(IShipRepository shipRepository)
        {
            _shipRepository = shipRepository;
        }
        public async Task Execute(Guid id)
        {
            var ship = await _shipRepository.FindById(id);
            if (ship is null)
            {
                throw new NotFoundException("le bateau n'existe pas");
            }

            await _shipRepository.DeleteShip(id);
        }
    }
}

