using FreeMarket.Domain.Classes;

namespace FreeMarket.Domain.Interfaces
{
    public interface IService<T> where T : class?
    {
        public Task<ServiceResponse<T[]>> FindAll();
        public Task<ServiceResponse<T>> FindOne(int id);
    }
}
