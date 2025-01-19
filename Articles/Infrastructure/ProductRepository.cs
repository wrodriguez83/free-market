using System.Net.Http.Json;
using ProductModule.Domain;
using FreeMarket.Domain.Interfaces;

namespace ProductModule.Infrastructure
{
    internal class ProductRepository : IRepository<Product>
    {
        private readonly HttpClient _httpClient;
        private readonly string apiURL = "https://fakestoreapi.com/products";
        public ProductRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Domain.Product[]> GetAll()
        {
            var response = await _httpClient.GetFromJsonAsync<Product[]>(apiURL);
            return response ?? [];
        }

        public async Task<Product?> GetOne(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<Product>($"{apiURL}/{id}");
            return response;
        }
    }
}
