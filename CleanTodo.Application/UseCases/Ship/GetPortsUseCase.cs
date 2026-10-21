using CleanTodo.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTodo.Application.UseCases
{
    public class GetPortsUseCase
    {
        private readonly IBrokerGateWay _gateway;

        public GetPortsUseCase(IBrokerGateWay gateway)
        {
            _gateway = gateway;
        }

        public Task<List<string>> ExecuteAsync() => _gateway.GetPortsAsync();
    }
}
