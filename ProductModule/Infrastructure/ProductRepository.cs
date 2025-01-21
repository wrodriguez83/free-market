using FreeMarket.Domain.Interfaces;
using ProductModule.Domain;

namespace ProductModule.Infrastructure
{
    public class ProductRepository(IRepository<Product> repository) : IModuleRepository<Product>
    {
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAll()
        {
            return repository.GetAll("products");
        }

        public Task<List<Product>> GetAllBy(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetOne(int id)
        {
            return repository.GetOne("products", id);
        }

        public Task<Product?> Upsert(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
