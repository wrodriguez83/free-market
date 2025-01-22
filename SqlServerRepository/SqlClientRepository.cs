using System.Data;
using FreeMarket.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace SqlServerRepository
{
    public abstract class SqlClientRepository<T>(IConfiguration configuration) : IRepository<T> where T:IEntity
    {
        private readonly SqlConnection connection = new(configuration.GetConnectionString("db"));
        public async Task<List<T>> GetAll(string entityName)
        {
            SqlCommand command = CreateCommand($"get_{entityName}");
            DataTable table = ExecuteGet(command);

            return await Task.FromResult(await Parse(table.Rows)).ConfigureAwait(false);
        }
        public async Task<List<T>> GetAllBy(string entityName, int id)
        {
            SqlCommand command = CreateCommand($"get_{entityName}");
            ParseGetParameters(id, command);
            DataTable table = ExecuteGet(command);
            return await Task.FromResult(await Parse(table.Rows)).ConfigureAwait(false);
        }
        public async Task<T?> GetOne(string entityName, int id)
        {
            SqlCommand command = CreateCommand($"get_{entityName}");
            ParseGetParameters(id, command);
            DataTable table = ExecuteGet(command);

            return await Task.FromResult((await Parse(table.Rows)).FirstOrDefault()).ConfigureAwait(false);
        }
        public async Task<T?> Upsert(string entityName, T entity)
        {
            SqlCommand command = CreateCommand($"upsert_{entityName}");
            ParseUpsertParameters(entity, command);
            int id = ExecuteUpsert(command);
            entity.Id = id;
            return await Task.FromResult(entity).ConfigureAwait(false);
        }
        public void Delete(string entityName, int id)
        {
            SqlCommand command = CreateCommand($"delete_{entityName}");
            ParseDeleteParameters(id, command);
            ExecuteDelete(command);
        }
        protected virtual DataTable ExecuteGet(SqlCommand command)
        {
            try
            {
                DataTable table = new();
                SqlDataAdapter adapter = new(command);
                adapter.Fill(table);

                return table;
            }
            finally
            {
                connection.Close();
            }
        }
        protected virtual int ExecuteUpsert(SqlCommand command)
        {
            try
            {
                connection.Open();
                int id = (int)command.ExecuteScalar();
                return id;
            }
            finally
            {
                connection.Close();
            }
        }

        protected virtual void ExecuteDelete(SqlCommand command)
        {
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
        protected virtual SqlCommand CreateCommand(string cmdText)
        {
            return new SqlCommand(cmdText, connection)
            {
                CommandType = CommandType.StoredProcedure
            };
        }
        protected abstract Task<List<T>> Parse(DataRowCollection rows);
        protected abstract void ParseGetParameters(int id, SqlCommand command);
        protected abstract void ParseUpsertParameters(T entity, SqlCommand command);
        protected abstract void ParseDeleteParameters(int id, SqlCommand command);
    }
}