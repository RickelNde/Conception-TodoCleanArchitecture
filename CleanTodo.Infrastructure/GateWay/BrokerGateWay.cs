using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Interfaces.Repositories;
using System.Net.Http.Json;

namespace CleanTodo.Infrastructure.GateWay
{
    public class BrokerGateWay : IBrokerGateWay
    {
        private readonly HttpClient _httpClient;

        public BrokerGateWay(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<string>> GetPortsAsync()
        {
            var response = await _httpClient.GetAsync("api/users");
   
            response.EnsureSuccessStatusCode();
            var ports = await response.Content.ReadFromJsonAsync<List<string>>();
            return ports ?? new List<string>();
        }

        public async Task SendShipAsync(string destination)
        {
            var response = await _httpClient.PostAsync(
                $"api/ship/sail/{Uri.EscapeDataString(destination)}", null);
            response.EnsureSuccessStatusCode();
        }
    }
}