using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Entities;
using CleanTodo.Domain.Interfaces.Repositories;
using FluentValidation;

namespace CleanTodo.Application.UseCases
{
    public class DockShipUseCase
    {
        private readonly IShipRepository _shipRepository;
        private readonly IValidator<DockShipDto> _validator;

        public DockShipUseCase(IShipRepository shipRepository, IValidator<DockShipDto> validator)
        {
            _shipRepository = shipRepository;
            _validator = validator;
        }

        public async Task<ShipDto> Execute(DockShipDto dto)
        {
            await _validator.ValidateAndThrowAsync(dto);
            var ships = await _shipRepository.GetAll();
            if (ships.Count >= 8)
            {
                throw new InvalidOperationException("impossible de recevoir des bateau le port est plein");
            }
            else { 

            var ship = new Ship(dto.Name, dto.Captain, dto.CrewSize, dto.GoldCargo, dto.CreatedBy);
            ship.Status = "docked";

            if (dto.CreatedAt != default)
            {
                ship.CreatedAt = dto.CreatedAt;
            }
            ship.LastModified = DateTime.UtcNow;
            
            

            await _shipRepository.AddShip(ship);
            

            return new ShipDto(ship);
            }
        }
    }
}