using Microsoft.Extensions.Configuration;

namespace TestHelper
{
    public class FakeStoreApiRepositoryMock<T>(HttpClient httpClient, IConfiguration configuration) : StoreApiRepository.FakeStoreApiRepository<T>(httpClient, configuration) { }
}
