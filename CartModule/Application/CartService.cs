using System.Net;
using CartModule.Domain;
using FreeMarket.Domain.Classes;
using FreeMarket.Domain.Interfaces;
using Microsoft.Data.SqlClient;

namespace CartModule.Application
{
    public class CartService(IModuleRepository<Cart> repository) : IService<Cart>
    {
        public Task<ServiceResponse<object>> Delete(int id)
        {
            try
            {
                repository.Delete(id);
                return Task.FromResult(ServiceResponse<object>.Send(null));
            }
            catch (SqlException ex)
            {
                return Task.FromResult(ServiceResponse<object>.SendError(ex.Message, HttpStatusCode.BadGateway));
            }
            catch (Exception)
            {
                return Task.FromResult(ServiceResponse<object>.SendError());
            }
        }

        public async Task<ServiceResponse<List<Cart>>> FindAll()
        {
            try
            {
                List<Cart> data = await repository.GetAll();
                return ServiceResponse<List<Cart>>.Send(data);
            }
            catch (SqlException ex)
            {
                return ServiceResponse<List<Cart>>.SendError(ex.Message, HttpStatusCode.BadGateway);
            }
            catch (Exception)
            {
                return ServiceResponse<List<Cart>>.SendError();
            }
        }
        public async Task<ServiceResponse<Cart>> FindOne(int id)
        {
            try
            {
                Cart? data = await repository.GetOne(id);
                return ServiceResponse<Cart>.Send(data);
            }
            catch (SqlException ex)
            {
                return ServiceResponse<Cart>.SendError(ex.Message, HttpStatusCode.BadGateway);
            }
            catch (Exception)
            {
                return ServiceResponse<Cart>.SendError();
            }
        }
        public async Task<ServiceResponse<Cart>> Upsert(Cart entity)
        {
            try
            {
                await repository.Upsert(entity);
                return ServiceResponse<Cart>.Send(entity);
            }
            catch (SqlException ex)
            {
                return ServiceResponse<Cart>.SendError(ex.Message, HttpStatusCode.BadGateway);
            }
            catch (Exception)
            {
                return ServiceResponse<Cart>.SendError();
            }
        }
    }
}
