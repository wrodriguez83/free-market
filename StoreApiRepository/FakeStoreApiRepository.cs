using System.Net.Http.Json;
using FreeMarket.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace StoreApiRepository
{
    public abstract class FakeStoreApiRepository<T>(HttpClient httpClient, IConfiguration configuration) : IRepository<T>
    {
        private readonly string? ApiURL = configuration.GetConnectionString("storeApi");
        public async Task<List<T>> GetAll(string entityName)
        {
            var response = await httpClient.GetFromJsonAsync<List<T>>($"{ApiURL}/{entityName}");
            return response ?? [];
        }
        public async Task<T?> GetOne(string entityName, int id)
        {
            var response = await httpClient.GetFromJsonAsync<T>($"{ApiURL}/{entityName}/{id}");
            return response;
        }
        public Task<T?> Upsert(string entityName, T entity)
        {
            throw new NotImplementedException();
        }
        public void Delete(string entityName, int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetAllBy(string entityName, int id)
        {
            throw new NotImplementedException();
        }
    }
}