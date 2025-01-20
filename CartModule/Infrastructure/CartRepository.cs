using System.Data;
using CartModule.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProductModule.Application;
using ProductModule.Domain;
using SqlServerRepository;

namespace CartModule.Infrastructure
{
    public class CartRepository(ProductService productService,CartItemRepository cartItemRepository, IConfiguration configuration) : SqlClientRepository<Cart>(configuration)
    {
        protected override string Entity => "carts";
        protected override void ParseDeleteParameters(int id, SqlCommand command)
        {
            command.Parameters.AddWithValue("@id", id);
        }
        protected override void ParseGetParameters(int id, SqlCommand command)
        {
            command.Parameters.AddWithValue("@id", id);
        }
        protected override void ParseUpsertParameters(Cart entity, SqlCommand command)
        {
            command.Parameters.AddWithValue("@id", entity.Id);
            command.Parameters.AddWithValue("@name", entity.Name);
        }
        protected override void PreUpsert(Cart entity)
        {
            cartItemRepository.Delete(entity.Id);
        }
        protected override async Task<Cart[]> Parse(DataRowCollection rows)
        {
            Cart[] currentCarts = [];

            foreach (DataRow item in rows)
            {
                ParseCart(currentCarts,item);
            }

            return await Task.FromResult(currentCarts).ConfigureAwait(false);
        }
        private void ParseCart(Cart[] currentCarts, DataRow row) {
            int id = row.Field<int>("id");
            string cartName = row.Field<string>("name") ?? "";
            DateTime updated_at = row.Field<DateTime>("updated_at");
            bool active = row.Field<bool>("active");

            Cart cart = new() { Id = id, Name = cartName, Products = [], LastUpdate = updated_at, Active= active };
            currentCarts.Append(cart);
        }

        private async Task ParseCartItem(Cart currentCart, DataRow row)
        {
            int id = row.Field<int>("itemId");
            int productId = row.Field<int>("productId");
            int quantity = row.Field<int>("quantity");
            double price = row.Field<int>("price");
            DateTime updated_at = row.Field<DateTime>("itemUpdated_at");
            Product? product = (await productService.FindOne(productId)).Data;

            Rating rating = new Rating() { Count = product?.Rating.Count ?? 0, Rate = product?.Rating.Rate ?? 0 };
            CartItem cartItem = new() { Id = id, Category = product?.Category ?? "", Description = product?.Description ?? "", Image = product?.Image ?? "", LastUpdate = updated_at, Price = price, Quantity = quantity, Rating = rating, Title = product?.Title ?? "" };

            currentCart.Products.Add(id,cartItem);
        }
    }
}