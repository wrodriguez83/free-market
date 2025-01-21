using CartModule.Domain;
using FreeMarket.Domain.Interfaces;

namespace CartModule.Infrastructure
{
    public class CartItemRepository(IRepository<CartItem> repository) : IModuleRepository<CartItem>
    {
        public void Delete(int id)
        {
            repository.Delete("cartItems", id);
        }

        public async Task<List<CartItem>> GetAll()
        {
            return await repository.GetAll("cartItems");
        }

        public async Task<List<CartItem>> GetAllBy(int id)
        {
            return await repository.GetAllBy("cartItems",id);
        }

        public Task<CartItem?> GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CartItem?> Upsert(CartItem entity)
        {
            return await repository.Upsert("cartItems", entity);
        }
    }
}