using System.Data;
using CartModule.Domain;
using FreeMarket.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ProductModule.Domain;
using SqlServerRepository;

namespace CartModule.Infrastructure
{
    public class SqlClientCartItemRepository(IService<Product> productService, IConfiguration configuration) : SqlClientRepository<CartItem>(configuration)
    {
        protected override async Task<List<CartItem>> Parse(DataRowCollection rows)
        {
            List<CartItem> currentCartItems = new();

            foreach (DataRow row in rows)
            {
                int id = row.Field<int>("id");
                int cartId = row.Field<int>("cartId");
                int productId = row.Field<int>("productId");
                int quantity = row.Field<int>("quantity");
                double price = row.Field<double>("price");
                DateTime updated_at = row.Field<DateTime>("updated_at");
                Product? product = (await productService.FindOne(productId)).Data;

                CartItem cartItem = new() { Id = id, LastUpdate = updated_at, Price = price, Quantity = quantity, CartId = cartId, Product = product };

                currentCartItems.Add(cartItem);
            }

            return await Task.FromResult(currentCartItems).ConfigureAwait(false);
        }

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
            command.Parameters.AddWithValue("@productId", entity.Product?.Id);
            command.Parameters.AddWithValue("@quantity", entity.Quantity);
            command.Parameters.AddWithValue("@price", entity.Price);
            command.Parameters.AddWithValue("@cartId", entity.CartId);
        }
    }
}