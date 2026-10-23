using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Interfaces.Repositories;

namespace CleanTodo.Application.UseCases
{
    public class SendShipUseCase
    {
        private readonly IBrokerGateWay _gateway;
        private readonly GetShipUseCase _getShipUseCase;
        private readonly DeleteShipUseCase _deleteShipUseCase;

        public SendShipUseCase(
            IBrokerGateWay gateway,
            GetShipUseCase getShipUseCase,
            DeleteShipUseCase deleteShipUseCase)
        {
            _gateway = gateway;
            _getShipUseCase = getShipUseCase;
            _deleteShipUseCase = deleteShipUseCase;
        }

        public async Task<bool> ExecuteAsync(Guid shipId, string destination)
        {
            var ship = await _getShipUseCase.Execute(shipId);

            var dockDto = new DockShipDto
            {
                Name = ship.Name,
                Captain=ship.Captain,
                CrewSize=ship.CrewSize,
                GoldCargo=ship.GoldCargo,
                Status = "sailing",
                CreatedBy=ship.CreatedBy,

            }; 

            var sent = await _gateway.SendShipAsync(dockDto, destination);
            if (!sent)
                return false;

            await _deleteShipUseCase.Execute(shipId); 
            return true;
        }
    }
}