using System.Net.Http.Json;
using CartModule.Domain;
using FreeMarket.Domain.Interfaces;

namespace CartModule.Infrastructure
{
    internal class CartRepository : IRepository<Cart>
    {
        private readonly HttpClient _httpClient;
        private readonly string apiURL = "https://fakestoreapi.com/products";
        public CartRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Domain.Cart[]> GetAll()
        {
            var response = await _httpClient.GetFromJsonAsync<Cart[]>(apiURL);
            return response ?? [];
        }

        public async Task<Cart?> GetOne(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<Cart>($"{apiURL}/{id}");
            return response;
        }
    }
}
