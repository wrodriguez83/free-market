namespace FreeMarket.Domain.Interfaces
{
    public interface IModuleRepository<T>
    {
        public Task<List<T>> GetAll();
        public Task<List<T>> GetAllBy(int id);
        public Task<T?> GetOne(int id);
        public Task<T?> Upsert(T entity);
        public void Delete(int id);
    }
}