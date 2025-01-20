using System.Data;
using CartModule.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProductModule.Application;
using ProductModule.Domain;
using SqlServerRepository;

namespace CartModule.Infrastructure
{
    public class CartItemRepository(ProductService productService, IConfiguration configuration) : SqlClientRepository<CartItem>(configuration)
    {
        protected override string Entity => "cartItems";
        protected override void ParseDeleteParameters(int id, SqlCommand command)
        {
            command.Parameters.AddWithValue("@cartId", id);
        }
        protected override void ParseGetParameters(int id, SqlCommand command)
        {
            command.Parameters.AddWithValue("@cartId", id);
        }
        protected override void ParseUpsertParameters(CartItem entity, SqlCommand command)
        {
            command.Parameters.AddWithValue("@id", entity.Id);
            command.Parameters.AddWithValue("@productId", entity.Id);
            command.Parameters.AddWithValue("@quantity", entity.Quantity);
            command.Parameters.AddWithValue("@price", entity.Price);
            command.Parameters.AddWithValue("@cartId", entity.CartId);
        }
        protected override async Task<CartItem[]> Parse(DataRowCollection rows)
        {
            CartItem[] currentCartItems = [];

            foreach (DataRow item in rows)
            {
                ParseCartItem(currentCartItems, item);
            }

            return await Task.FromResult(currentCartItems).ConfigureAwait(false);
        }
        private async void ParseCartItem(CartItem[] currentCartItems, DataRow row)
        {
            int id = row.Field<int>("id");
            int cartId = row.Field<int>("cartId");
            int productId = row.Field<int>("productId");
            int quantity = row.Field<int>("quantity");
            double price = row.Field<int>("price");
            DateTime updated_at = row.Field<DateTime>("updated_at");
            Product? product = (await productService.FindOne(productId)).Data;

            Rating rating = new() { Count = product?.Rating.Count ?? 0, Rate = product?.Rating.Rate ?? 0 };
            CartItem cartItem = new() { Id = id, Category = product?.Category ?? "", Description = product?.Description ?? "", Image = product?.Image ?? "", LastUpdate = updated_at, Price = price, Quantity = quantity, Rating = rating, Title = product?.Title ?? "",CartId=cartId };

            currentCartItems.Append(cartItem);
        }
    }
}