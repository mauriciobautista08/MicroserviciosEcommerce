using Api.Gateway.Models;
using Api.Gateway.Models.Order.Commands;
using Api.Gateway.Models.Order.DTO;
using Api.Gateway.Proxies.config;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Gateway.Proxies
{

    public interface IOrderProxy
    {
        Task<DataCollection<OrderDTO>> GetAllAsync(int page, int take, IEnumerable<int> clients = null);
        Task<OrderDTO> GetAsync(int id);
        Task CreateAsync(OrderCreateCommand command);

    }
    public class OrderProxy : IOrderProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public OrderProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient.AddBearerToken(httpContextAccessor);
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<DataCollection<OrderDTO>> GetAllAsync(int page, int take, IEnumerable<int> clients = null)
        {
            var ids = string.Join(',', clients ?? new List<int>());

            var request = await _httpClient.GetAsync($"{_apiUrls.OrderUrl}orders?page={page}&take={take}&ids={ids}");

            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<OrderDTO>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<OrderDTO> GetAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.OrderUrl}orders/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<OrderDTO>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task CreateAsync(OrderCreateCommand command)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(command),
                Encoding.UTF8,
                "application/json");

            var request = await _httpClient.PostAsync($"{_apiUrls.OrderUrl}orders", content);
            request.EnsureSuccessStatusCode();
        }
    }
}
