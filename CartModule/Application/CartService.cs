using CartModule.Domain;
using CartModule.Infrastructure;
using FreeMarket.Domain.Classes;
using FreeMarket.Domain.Interfaces;

namespace CartModule.Application
{
    public class CartService:IService<Cart>
    {
        private readonly CartRepository repository = new(new HttpClient());

        public async Task<ServiceResponse<Cart[]>> FindAll()
        {
            try
            {
                Cart[] data = await repository.GetAll();
                return ServiceResponse<Cart[]>.Send(data);
            }
            catch (HttpRequestException ex)
            {
                return ServiceResponse<Cart[]>.SendError(ex.Message,ex.StatusCode);
            }
            catch (Exception)
            {
                return ServiceResponse<Cart[]>.SendError();
            }
        }

        public async Task<ServiceResponse<Cart>> FindOne(int id)
        {
            try
            {
                Cart? data = await repository.GetOne(id);
                return ServiceResponse<Cart>.Send(data);
            }
            catch (HttpRequestException ex)
            {
                return ServiceResponse<Cart>.SendError(ex.Message, ex.StatusCode);
            }
            catch (Exception)
            {
                return ServiceResponse<Cart>.SendError();
            }
        }
    }
}
