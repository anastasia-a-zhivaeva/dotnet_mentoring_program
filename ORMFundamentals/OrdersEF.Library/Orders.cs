using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace OrdersEF.Library
{
    public class Orders
    {
        private readonly OrdersContext _ordersContext;

        public Orders(string connectionString)
        {
            _ordersContext = new OrdersContext(connectionString);
        }

        public int Add(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            var addedOrder = _ordersContext.Orders.Add(order);
            _ordersContext.SaveChanges();
            return addedOrder.Entity.Id;
        }

        public Order Get(int id)
        {
            var order = _ordersContext.Orders.Find(id);

            if (order == null)
                throw new KeyNotFoundException();
            return order;
        }

        public void Remove(int id)
        {
            var order = Get(id);
            _ordersContext.Orders.Remove(order);
            _ordersContext.SaveChanges();
        }

        public void Update(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _ordersContext.Orders.Update(order);
            _ordersContext.SaveChanges();
        }

        public IEnumerable<Order> GetAll(int month = 0, int year = 0, string status = null, int productId = 0)
        {
            var parameters = new List<SqlParameter>();
            if (month > 0)
            {
                parameters.Add(new SqlParameter { ParameterName = "@Month", SqlDbType = System.Data.SqlDbType.Int, Value = month });
            }
            if (year > 0)
            {
                parameters.Add(new SqlParameter { ParameterName = "@Year", SqlDbType = System.Data.SqlDbType.Int, Value = year });
            }
            if (!string.IsNullOrWhiteSpace(status))
            {
                parameters.Add(new SqlParameter { ParameterName = "@Status", SqlDbType = System.Data.SqlDbType.VarChar, Value = status });
            }
            if (productId > 0)
            {
                parameters.Add(new SqlParameter{ ParameterName = "@ProductId", SqlDbType = System.Data.SqlDbType.Int, Value = productId });
            }

            var parametersTemplate = CreateParametersTemplate(parameters);
            return _ordersContext.Orders.FromSqlRaw($"EXEC [dbo].[FetchOrders] {parametersTemplate}", parameters.ToArray()).ToList();
        }

        public void BulkDelete(int month = 0, int year = 0, string status = null, int productId = 0)
        {
            var parameters = new List<SqlParameter>();
            if (month > 0)
            {
                parameters.Add(new SqlParameter{ ParameterName = "@Month", SqlDbType = System.Data.SqlDbType.Int, Value = month });
            }
            if (year > 0)
            {
                parameters.Add(new SqlParameter { ParameterName = "@Year", SqlDbType = System.Data.SqlDbType.Int, Value = year });
            }
            if (!string.IsNullOrWhiteSpace(status))
            {
                parameters.Add(new SqlParameter { ParameterName = "@Status", SqlDbType = System.Data.SqlDbType.VarChar, Value = status });
            }
            if (productId > 0)
            {
                parameters.Add(new SqlParameter { ParameterName = "@ProductId", SqlDbType = System.Data.SqlDbType.Int, Value = productId });
            }

            var parametersTemplate = CreateParametersTemplate(parameters);
            _ordersContext.Database.ExecuteSqlRaw($"EXEC [dbo].[BulkDeleteOrders] {parametersTemplate}", parameters.ToArray());
        }

        private string CreateParametersTemplate(List<SqlParameter> parameters)
        {
            return string.Join(',', parameters.ConvertAll(p => $"{p.ParameterName} = {p.ParameterName}"));
        }
    }
}