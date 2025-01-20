using System.Net.Http.Json;
using FreeMarket.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace StoreApiRepository
{
    public abstract class FakeStoreApiRepository<T>(HttpClient httpClient, IConfiguration configuration) : IRepository<T>
    {
        private readonly string? ApiURL = configuration.GetConnectionString("storeApi");
        protected virtual string Entity { get { return ""; } }
        public async Task<T[]> GetAll()
        {
            var response = await httpClient.GetFromJsonAsync<T[]>($"{ApiURL}/{Entity}");
            return response ?? [];
        }
        public async Task<T?> GetOne(int id)
        {
            var response = await httpClient.GetFromJsonAsync<T>($"{ApiURL}/{Entity}/{id}");
            return response;
        }
        public Task<T> Upsert(T entity)
        {
            throw new NotImplementedException();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}