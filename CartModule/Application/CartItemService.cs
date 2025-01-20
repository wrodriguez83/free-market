using System.Net;
using CartModule.Domain;
using CartModule.Infrastructure;
using FreeMarket.Domain.Classes;
using FreeMarket.Domain.Interfaces;
using Microsoft.Data.SqlClient;

namespace CartModule.Application
{
    public class CartItemService(CartItemRepository repository) : IService<CartItem>
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

        public async Task<ServiceResponse<CartItem[]>> FindAll()
        {
            try
            {
                CartItem[] data = await repository.GetAll();
                return ServiceResponse<CartItem[]>.Send(data);
            }
            catch (SqlException ex)
            {
                return ServiceResponse<CartItem[]>.SendError(ex.Message, HttpStatusCode.BadGateway);
            }
            catch (Exception)
            {
                return ServiceResponse<CartItem[]>.SendError();
            }
        }

        public async Task<ServiceResponse<CartItem>> FindOne(int id)
        {
            try
            {
                CartItem? data = await repository.GetOne(id);
                return ServiceResponse<CartItem>.Send(data);
            }
            catch (SqlException ex)
            {
                return ServiceResponse<CartItem>.SendError(ex.Message, HttpStatusCode.BadGateway);
            }
            catch (Exception)
            {
                return ServiceResponse<CartItem>.SendError();
            }
        }

        public async Task<ServiceResponse<CartItem>> Upsert(CartItem entity)
        {
            try
            {
                CartItem? data = await repository.Upsert(entity);
                return ServiceResponse<CartItem>.Send(data);
            }
            catch (SqlException ex)
            {
                return ServiceResponse<CartItem>.SendError(ex.Message, HttpStatusCode.BadGateway);
            }
            catch (Exception)
            {
                return ServiceResponse<CartItem>.SendError();
            }
        }
    }
}
