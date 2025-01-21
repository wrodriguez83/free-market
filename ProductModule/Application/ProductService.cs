using FreeMarket.Domain.Classes;
using FreeMarket.Domain.Interfaces;
using ProductModule.Domain;

namespace ProductModule.Application
{
    public class ProductService(IModuleRepository<Product> repository) : IService<Product>
    {
        public async Task<ServiceResponse<List<Product>>> FindAll()
        {
            try
            {
                List<Product> data = await repository.GetAll();
                return ServiceResponse<List<Product>>.Send(data);
            }
            catch (HttpRequestException ex)
            {
                return ServiceResponse<List<Product>>.SendError(ex.Message, ex.StatusCode);
            }
            catch (Exception)
            {
                return ServiceResponse<List<Product>>.SendError();
            }
        }

        public async Task<ServiceResponse<Product>> FindOne(int id)
        {
            try
            {
                Product? data = await repository.GetOne(id);
                return ServiceResponse<Product>.Send(data);
            }
            catch (HttpRequestException ex)
            {
                return ServiceResponse<Product>.SendError(ex.Message, ex.StatusCode);
            }
            catch (Exception)
            {
                return ServiceResponse<Product>.SendError();
            }
        }

        public Task<ServiceResponse<Product>> Upsert(Product entity)
        {
            throw new NotImplementedException();
        }
        public Task<ServiceResponse<object>> Delete(int id)
        {
            throw new NotImplementedException();
        }
    }
}
