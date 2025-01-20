using FreeMarket.Domain.Classes;
using FreeMarket.Domain.Interfaces;
using ProductModule.Domain;
using ProductModule.Infrastructure;

namespace ProductModule.Application
{
    public class ProductService(ProductRepository repository):IService<Product>
    {
        public Task<ServiceResponse<object>> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<Product[]>> FindAll()
        {
            try
            {
                Product[] data = await repository.GetAll();
                return ServiceResponse<Product[]>.Send(data);
            }
            catch (HttpRequestException ex)
            {
                return ServiceResponse<Product[]>.SendError(ex.Message,ex.StatusCode);
            }
            catch (Exception)
            {
                return ServiceResponse<Product[]>.SendError();
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
    }
}
