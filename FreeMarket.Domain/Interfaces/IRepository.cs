namespace FreeMarket.Domain.Interfaces
{
    public interface IRepository<T>
    {
        public Task<T[]> GetAll();
        public Task<T?> GetOne(int id);
        public Task<T> Upsert(T entity);
        public void Delete(int id);
    }
}
