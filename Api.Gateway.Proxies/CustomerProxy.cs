using Api.Gateway.Models;
using Api.Gateway.Models.Customer.DTO;
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
    public interface ICustomerProxy
    {
        Task<DataCollection<ClientDTO>> GetAllAsync(int page, int take, IEnumerable<int> clients = null);
        Task<ClientDTO> GetAsync(int id);

    }

    public class CustomerProxy: ICustomerProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public CustomerProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccesor)
        {
            httpClient.AddBearerToken(httpContextAccesor);
            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<DataCollection<ClientDTO>> GetAllAsync(int page, int take, IEnumerable<int> clients = null)
        {
            var ids = string.Join(',', clients ?? new List<int>());

            var request = await _httpClient.GetAsync($"{_apiUrls.CustomerUrl}v1/clients?page={page}&take={take}&ids={ids}");

            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<ClientDTO>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<ClientDTO> GetAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CustomerUrl}v1/clients/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ClientDTO>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
