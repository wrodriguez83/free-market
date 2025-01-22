using System.Data;
using CartModule.Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using SqlServerRepository;

namespace CartModule.Infrastructure
{
    public class SqlClientCartRepository(IConfiguration configuration) : SqlClientRepository<Cart>(configuration)
    {
        protected override async Task<List<Cart>> Parse(DataRowCollection rows)
        {
            List<Cart> currentCarts = new();

            foreach (DataRow row in rows)
            {
                int id = row.Field<int>("id");
                string cartName = row.Field<string>("name") ?? "";
                DateTime updated_at = row.Field<DateTime>("updated_at");

                Cart cart = new() { Id = id, Name = cartName, Items = [], LastUpdate = updated_at };
                currentCarts.Add(cart);
            }

            return await Task.FromResult(currentCarts).ConfigureAwait(false);
        }

        protected override void ParseDeleteParameters(int id, SqlCommand command)
        {
            if (id > 0)
            {
                command.Parameters.AddWithValue("@id", id);
            }
        }
        protected override void ParseGetParameters(int id, SqlCommand command)
        {
            if (id > 0)
            {
                command.Parameters.AddWithValue("@id", id);
            }
        }
        protected override void ParseUpsertParameters(Cart entity, SqlCommand command)
        {
            if (entity.Id > 0)
            {
                command.Parameters.AddWithValue("@id", entity.Id);
            }

            command.Parameters.AddWithValue("@name", entity.Name);
        }
    }
}