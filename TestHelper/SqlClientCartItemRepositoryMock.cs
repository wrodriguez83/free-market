using CartModule.Domain;
using CartModule.Infrastructure;
using Microsoft.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;
using FreeMarket.Domain.Interfaces;
using ProductModule.Domain;

namespace TestHelper
{
    public class SqlClientCartItemRepositoryMock(IService<Product> productService, IConfiguration configuration) : SqlClientCartItemRepository(productService, configuration)
    {
        public static List<CartItem> Data = new();
        public SqlCommand? SqlCommand;
        protected override DataTable ExecuteGet(SqlCommand command)
        {
            DataTable dt = new DataTable();

            DtoToDataRow(dt);
            return dt;
        }
        protected override int ExecuteUpsert(SqlCommand command)
        {
            if (Data[0].Id == 0)
            {
                return new Random().Next(1, 100);
            }

            return Data[0].Id;
        }

        protected override void ExecuteDelete(SqlCommand command)
        {
            
        }

        protected override SqlCommand CreateCommand(string cmdText)
        {
            SqlCommand = base.CreateCommand(cmdText);
            return SqlCommand;
        }

        private void DtoToDataRow(DataTable dt)
        {
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("cartId", typeof(int));
            dt.Columns.Add("quantity", typeof(int));
            dt.Columns.Add("price", typeof(decimal));
            dt.Columns.Add("productId", typeof(int));
            dt.Columns.Add("updated_at", typeof(DateTime));

            foreach (CartItem item in Data)
            {
                DataRow row = dt.NewRow();
                row.SetField("id", item.Id);
                row.SetField("cartId", item.CartId);
                row.SetField("quantity", item.Quantity);
                row.SetField("price", item.Price);
                row.SetField("productId", item.Product?.Id);
                row.SetField("updated_at", item.LastUpdate);

                dt.Rows.Add(row);
            }
        }
    }
}
