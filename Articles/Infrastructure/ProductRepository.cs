using Microsoft.Extensions.Configuration;
using ProductModule.Domain;
using StoreApiRepository;

namespace ProductModule.Infrastructure
{
    public class ProductRepository(HttpClient httpClient, IConfiguration configuration) : FakeStoreApiRepository<Product>(httpClient, configuration)
    {
        protected override string Entity => "products";
    }
}
