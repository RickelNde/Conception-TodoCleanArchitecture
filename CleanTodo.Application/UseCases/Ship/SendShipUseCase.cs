using CleanTodo.Domain.Exceptions;
using CleanTodo.Domain.Interfaces.Repositories;
using CleanTodo.Domain.DTOS;

namespace CleanTodo.Application.UseCases
{
    public class SendShipUseCase
    {
        private readonly IBrokerGateWay _gateway;

        public SendShipUseCase(IBrokerGateWay gateway)
        {
            _gateway = gateway;
        }

        public async Task ExecuteAsync(string destination)
        {
            var ports = await _gateway.GetPortsAsync();
            if (!ports.Contains(destination))
                throw new ArgumentException("Destination invalide.");

            await _gateway.SendShipAsync(destination);
        }
    }
}