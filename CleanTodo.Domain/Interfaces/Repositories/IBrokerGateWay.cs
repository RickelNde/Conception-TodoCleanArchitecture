using CleanTodo.Domain.DTOS;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanTodo.Domain.Interfaces.Repositories
{
    public interface IBrokerGateWay
    {
        Task<List<string>> GetPortsAsync();
        Task SendShipAsync(string destination);
    }
}
