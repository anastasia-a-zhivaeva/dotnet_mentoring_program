using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace OrdersDapper.Library
{
    public class Orders
    {
        private readonly string _connectionString;

        public Orders(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int Add(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            using (var connection = new SqlConnection(_connectionString))
            {
                string commandString =
                    "INSERT INTO [dbo].[Order] (Status, CreatedDate, UpdatedDate, ProductId) " +
                    "OUTPUT INSERTED.ID " +
                    "VALUES (@Status, @CreatedDate, @UpdatedDate, @ProductId)";

                return connection.ExecuteScalar<int>(commandString, order);
            }
        }

        public Order Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string commandString = "SELECT * FROM [dbo].[Order] WHERE ID = @Id";

                var order = connection.QuerySingleOrDefault<Order>(commandString, new { Id = id });

                if (order == null)
                    throw new KeyNotFoundException();

                return order;
            }
        }

        public void Remove(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string commandString = "DELETE FROM [dbo].[Order] WHERE Id = @Id";

                var numberOfRows = connection.Execute(commandString, new { Id = id });

                if (numberOfRows == 0)
                {
                    throw new KeyNotFoundException();
                }
            }
        }

        public void Update(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            using (var connection = new SqlConnection(_connectionString))
            {
                string commandString =
                    "UPDATE [dbo].[Order] " +
                    "SET Status = @Status, CreatedDate = @CreatedDate, UpdatedDate = @UpdatedDate, ProductId = @ProductId " +
                    "WHERE Id = @Id";

                var numberOfRows = connection.Execute(commandString, order);

                if (numberOfRows == 0)
                {
                    throw new KeyNotFoundException();
                }
            }
        }

        public IEnumerable<Order> GetAll(int month = 0, int year = 0, string status = null, int productId = 0)
        {
            var parameters = new DynamicParameters();
            if (month > 0)
            {
               parameters.Add("@Month", month);
            }
            if (year > 0)
            {
               parameters.Add("@Year", year);
            }
            if (!string.IsNullOrWhiteSpace(status))
            {
                parameters.Add("@Status", status);
            }
            if (productId > 0)
            {
                parameters.Add("@ProductId", productId);
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                string commandString = "dbo.FetchOrders";
                var orders = connection.Query<Order>(commandString, parameters, commandType: CommandType.StoredProcedure);
                return orders;
            }
        }

        public void BulkDelete(int month = 0, int year = 0, string status = null, int productId = 0)
        {
            var parameters = new DynamicParameters();
            if (month > 0)
            {
                parameters.Add("@Month", month);
            }
            if (year > 0)
            {
                parameters.Add("@Year", year);
            }
            if (!string.IsNullOrWhiteSpace(status))
            {
                parameters.Add("@Status", status);
            }
            if (productId > 0)
            {
                parameters.Add("@ProductId", productId);
            }

            using (var connection = new SqlConnection(_connectionString))
            {
                string commandString = "dbo.BulkDeleteOrders";
                connection.Query<Order>(commandString, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}