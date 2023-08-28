using Dapper;
using Microsoft.Data.SqlClient;

namespace OrdersDapper.Library
{
    public class Products
    {
        private readonly string _connectionString;

        public Products(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Product Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string commandString = "SELECT * FROM [dbo].[Product] WHERE ID = @Id";

                var product = connection.QuerySingleOrDefault<Product>(commandString, new { Id = id });

                if (product == null)
                    throw new KeyNotFoundException();

                return product;
            }
        }

        public int Add(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            using (var connection = new SqlConnection(_connectionString))
            {
                string commandString =
                    "INSERT INTO [dbo].[Product] (Name, Description, Weight, Height, Width, Length) " +
                    "OUTPUT INSERTED.ID " +
                    "VALUES (@Name, @Description, @Weight, @Height, @Width, @Length)";

                return connection.ExecuteScalar<int>(commandString, product);
            }
        }

        public void Remove(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string commandString = "DELETE FROM [dbo].[Product] WHERE Id = @Id";

                var numberOfRows = connection.Execute(commandString, new { Id = id });

                if (numberOfRows == 0)
                {
                    throw new KeyNotFoundException();
                }
            }
        }

        public void Update(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            using (var connection = new SqlConnection(_connectionString))
            {
                string commandString =
                    "UPDATE [dbo].[Product] " +
                    "SET Name = @Name, Description = @Description, Weight = @Weight, Height = @Height, Width = @Width, Length = @Length " +
                    "WHERE Id = @Id";

                var numberOfRows = connection.Execute(commandString, product);

                if (numberOfRows == 0)
                {
                    throw new KeyNotFoundException();
                }
            }
        }

        public IEnumerable<Product> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string commandString = $"SELECT * FROM [dbo].[Product]";
                return connection.Query<Product>(commandString);
            }
        }
    }
}