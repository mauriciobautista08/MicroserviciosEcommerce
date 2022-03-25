using Api.Gateway.Models;
using Api.Gateway.Models.Catalog.DTO;
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
    public interface ICatalogProxy
    {
        Task<DataCollection<ProductDTO>> GetAllAsync(int page, int take, IEnumerable<int> clients = null);
        Task<ProductDTO> GetAsync(int id);

    }

    public class CatalogProxy : ICatalogProxy
    {
        private readonly ApiUrls _urls;
        private readonly HttpClient _httpClient;

        public CatalogProxy(HttpClient httpClient, IOptions<ApiUrls> apiUrls, IHttpContextAccessor httpContextAccessor)
        {
            httpClient.AddBearerToken(httpContextAccessor);
            _httpClient = httpClient;
            _urls = apiUrls.Value;
        }

        public async Task<DataCollection<ProductDTO>> GetAllAsync(int page, int take, IEnumerable<int> clients = null)
        {
            var ids = string.Join(',', clients ?? new List<int>());

            var request = await _httpClient.GetAsync($"{_urls.CatalogUrl}v1/products?page={page}&take={take}&ids={ids}");

            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<ProductDTO>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }

        public async Task<ProductDTO> GetAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_urls.CatalogUrl}v1/products/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ProductDTO>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            );
        }
    }
}
