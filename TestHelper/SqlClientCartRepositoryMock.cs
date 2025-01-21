using CartModule.Domain;
using CartModule.Infrastructure;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace TestHelper
{
    public class SqlClientCartRepositoryMock(IConfiguration configuration) : SqlClientCartRepository(configuration)
    {
        public static List<Cart> Data = new();
        public SqlCommand? SqlCommand;
        protected override DataTable RunQuery(SqlCommand command)
        {
            DataTable dt = new DataTable();

            DtoToDataRow(dt);
            return dt;
        }

        protected override SqlCommand CreateCommand(string cmdText)
        {
            SqlCommand = base.CreateCommand(cmdText);
            return SqlCommand;
        }

        private void DtoToDataRow(DataTable dt)
        {
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("name", typeof(string));
            dt.Columns.Add("updated_at", typeof(DateTime));
            foreach (Cart item in Data)
            {
                DataRow row = dt.NewRow();
                row.SetField("id", item.Id);
                row.SetField("name", item.Name);
                row.SetField("updated_at", item.LastUpdate);

                dt.Rows.Add(row);
            }
        }
    }
}
