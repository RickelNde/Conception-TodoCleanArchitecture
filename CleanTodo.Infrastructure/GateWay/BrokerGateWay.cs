using CleanTodo.Domain.DTOS;
using CleanTodo.Domain.Interfaces.Repositories;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;

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
            var json = await _httpClient.GetFromJsonAsync<JsonElement>("api/users");
            return json.GetProperty("users").Deserialize<List<string>>()!;
        }

        public async Task<bool> SendShipAsync(DockShipDto ship, string destination)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/ship/sail/{destination}", ship);

            return response.IsSuccessStatusCode;
        }
    }
}