using CartModule.Domain;
using FreeMarket.Domain.Interfaces;

namespace CartModule.Infrastructure
{
    public class CartRepository(IRepository<Cart> repository, IModuleRepository<CartItem> cardItemRepository) : IModuleRepository<Cart>
    {
        public void Delete(int id)
        {
            repository.Delete("carts", id);
            cardItemRepository.Delete(id);
        }

        public async Task<List<Cart>> GetAll()
        {
            List<Cart> carts = await repository.GetAll("carts");
            List<CartItem> items = await cardItemRepository.GetAll();

            foreach (Cart cart in carts)
            {
                List<CartItem> itemsOfCart = items.FindAll(x => x.Id == cart.Id);
                cart.Products = itemsOfCart;
            }

            return carts;
        }

        public async Task<List<Cart>> GetAllBy(int id)
        {
            List<Cart> carts = await repository.GetAllBy("carts", id);
            List<CartItem> items = await cardItemRepository.GetAllBy(id);

            foreach (Cart cart in carts)
            {
                List<CartItem> itemsOfCart = items.FindAll(x => x.Id == cart.Id);
                cart.Products = itemsOfCart;
            }

            return carts;
        }

        public async Task<Cart?> GetOne(int id)
        {
            Cart? cart = await repository.GetOne("carts", id);

            if (cart != null)
            {
                List<CartItem> items = await cardItemRepository.GetAllBy(id);
                cart.Products = items;
            }

            return cart;
        }

        public async Task<Cart?> Upsert(Cart entity)
        {
            cardItemRepository.Delete(entity.Id);
            foreach (CartItem cartItem in entity.Products)
            {
                await cardItemRepository.Upsert(cartItem);
            }
            await repository.Upsert("carts", entity);
            return await GetOne(entity.Id);
        }
    }
}