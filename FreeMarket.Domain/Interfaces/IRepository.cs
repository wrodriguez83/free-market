namespace FreeMarket.Domain.Interfaces
{
    public interface IRepository<T>
    {
        public Task<List<T>> GetAll(string entityName);
        public Task<List<T>> GetAllBy(string entityName, int id);
        public Task<T?> GetOne(string entityName, int id);
        public Task<T?> Upsert(string entityName, T entity);
        public void Delete(string entityName, int id);
    }
}