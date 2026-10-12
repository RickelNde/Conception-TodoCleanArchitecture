using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;
using FluentValidation;

namespace CleanTodo.Application.UseCases
{
    public class UpdateShipUseCase
    {
        private readonly IShipRepository _shipRepository;
        private readonly IValidator<UpdateShipDto> _validator;

        public UpdateShipUseCase(IShipRepository shipRepository, IValidator<UpdateShipDto> validator)
        {
            _shipRepository = shipRepository;
            _validator = validator;
        }

        public async Task<ShipDto> Execute(Guid id, UpdateShipDto dto)
        {
            await _validator.ValidateAndThrowAsync(dto);

            var ship = await _shipRepository.FindById(id);

            if (ship == null)
            {
                throw new NotFoundException("le bateau n'existee pas");
            }

            ship.Name = dto.Name;
            ship.Captain = dto.Captain;
            ship.CrewSize = dto.CrewSize;
            ship.GoldCargo = dto.GoldCargo;
            ship.Status = dto.Status;
            ship.LastModified = DateTime.UtcNow;

            await _shipRepository.UpdateShip(ship);

            return new ShipDto(ship);
        }
    }
}