using System.Data;
using FreeMarket.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SqlServerRepository
{
    public abstract class SqlClientRepository<T>(IConfiguration configuration) : IRepository<T>
    {
        private readonly SqlConnection connection = new(configuration.GetConnectionString("db"));
        public async Task<List<T>> GetAll(string entityName)
        {
            SqlCommand command = CreateCommand($"get_{entityName}");
            DataTable table = RunQuery(command);

            return await Task.FromResult(await Parse(table.Rows)).ConfigureAwait(false);
        }
        public async Task<T?> GetOne(string entityName, int id)
        {
            SqlCommand command = CreateCommand($"get_{entityName}");
            ParseGetParameters(id, command);
            DataTable table = RunQuery(command);

            return await Task.FromResult((await Parse(table.Rows)).FirstOrDefault()).ConfigureAwait(false);
        }
        public async Task<T?> Upsert(string entityName, T entity)
        {
            PreUpsert(entity);
            SqlCommand command = CreateCommand($"upsert_{entityName}");
            ParseUpsertParameters(entity, command);
            RunQuery(command);

            return await Task.FromResult(entity).ConfigureAwait(false);
        }
        public void Delete(string entityName, int id)
        {
            SqlCommand command = CreateCommand($"delete_{entityName}");
            ParseDeleteParameters(id, command);
            RunQuery(command);
        }
        protected virtual DataTable RunQuery(SqlCommand command)
        {
            DataTable table = new();
            SqlDataAdapter adapter = new(command);
            adapter.Fill(table);

            return table;
        }

        public async Task<List<T>> GetAllBy(string entityName, int id)
        {
            SqlCommand command = CreateCommand($"get_{entityName}");
            ParseGetParameters(id, command);
            DataTable table = RunQuery(command);
            return await Task.FromResult(await Parse(table.Rows)).ConfigureAwait(false);
        }

        protected virtual SqlCommand CreateCommand(string cmdText)
        {
            return new SqlCommand(cmdText, connection);
        }

        protected virtual void PreUpsert(T entity) { }
        protected abstract Task<List<T>> Parse(DataRowCollection rows);
        protected abstract void ParseGetParameters(int id, SqlCommand command);
        protected abstract void ParseUpsertParameters(T entity, SqlCommand command);
        protected abstract void ParseDeleteParameters(int id, SqlCommand command);
    }
}