using System.Data;
using FreeMarket.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SqlServerRepository
{
    public abstract class SqlClientRepository<T>(IConfiguration configuration) : IRepository<T>
    {
        private readonly SqlConnection connection = new(configuration.GetConnectionString("db"));
        protected virtual string Entity { get { return ""; } }
        public async Task<T[]> GetAll()
        {
            SqlCommand command = new($"get_{Entity}", connection);
            DataTable table = RunQuery(command);

            return await Task.FromResult(await Parse(table.Rows)).ConfigureAwait(false);
        }
        public async Task<T?> GetOne(int id)
        {
            SqlCommand command = new($"get_{Entity}", connection);
            ParseGetParameters(id, command);
            DataTable table = RunQuery(command);

            return await Task.FromResult((await Parse(table.Rows)).FirstOrDefault()).ConfigureAwait(false);
        }
        public async Task<T> Upsert(T entity)
        {
            PreUpsert(entity);
            SqlCommand command = new($"upsert_{Entity}", connection);
            ParseUpsertParameters(entity, command);
            DataTable table = RunQuery(command);

            return await Task.FromResult(entity).ConfigureAwait(false);
        }
        public void Delete(int id)
        {
            SqlCommand command = new($"delete_{Entity}", connection);
            ParseDeleteParameters(id, command);
            DataTable table = RunQuery(command);
        }
        private DataTable RunQuery(SqlCommand command)
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new(command);
            adapter.Fill(table);

            return table;
        }

        protected virtual void PreUpsert(T entity) { }
        protected abstract Task<T[]> Parse(DataRowCollection rows);
        protected abstract void ParseGetParameters(int id, SqlCommand command);
        protected abstract void ParseUpsertParameters(T entity, SqlCommand command);
        protected abstract void ParseDeleteParameters(int id, SqlCommand command);
    }
}
