using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Interfaces.Repositories;
using FluentValidation;


namespace CleanTodo.Application.UseCases
{
    public class CreateShipUseCase
    {
        private readonly IShipRepository _shipRepository;
        private readonly IValidator<CreateShipDto> _validator;

        public CreateShipUseCase(IShipRepository shipRepository, IValidator<CreateShipDto> validator)
        {
            _shipRepository = shipRepository;
            _validator = validator;
        }
        public async Task<ShipDto> Execute(CreateShipDto createShipDto, string createdBy)
        {
            await _validator.ValidateAndThrowAsync(createShipDto);

            var ship = new Ship(
                createShipDto.Name,
                createShipDto.Captain,
                createShipDto.CrewSize,
                createShipDto.GoldCargo,
                createdBy);

            var createdShip = await _shipRepository.AddShip(ship);
            return new ShipDto(createdShip);
        }
    }
}
