using System.Collections;
using Microsoft.Data.SqlClient;

namespace Orders.Library
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
                connection.Open();
                string commandString = $"SELECT * FROM [dbo].[Product] WHERE ID = {id};";

                SqlCommand command = new SqlCommand(commandString, connection);
                var reader = command.ExecuteReader();

                Product product = new Product();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        product.Id = id;
                        product.Name = reader.GetString(1);
                        product.Description = reader.GetString(2);
                        product.Weight = reader.GetDouble(3);
                        product.Height = reader.GetDouble(4);
                        product.Width = reader.GetDouble(5);
                        product.Length = reader.GetDouble(6);
                    }
                }
                else
                {
                   throw new KeyNotFoundException();
                }
                reader.Close();

                connection.Close();

                return product;
            }
        }

        public int Add(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            using(var connection = new SqlConnection(_connectionString))
            {
                string commandString =
                    "INSERT INTO [dbo].[Product] (Name, Description, Weight, Height, Width, Length) " +
                    "OUTPUT INSERTED.ID " +
                    $"VALUES ('{product.Name}', '{product.Description}', {product.Weight}, {product.Height}, {product.Width}, {product.Length});";

                SqlCommand command = new SqlCommand(commandString, connection);

                connection.Open();

                int insertedId = (int)command.ExecuteScalar();

                connection.Close();

                return insertedId;
            }
        }

        public void Remove(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string commandString = $"DELETE FROM [dbo].[Product] WHERE Id = {id} ";

                SqlCommand command = new SqlCommand(commandString, connection);

                connection.Open();

                var numberOfRows = command.ExecuteNonQuery();

                connection.Close();

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
                    $"SET Name = '{product.Name}', Description = '{product.Description}', Weight = {product.Weight}, Height = {product.Height}, Width = {product.Width}, Length = {product.Length}" +
                    $"WHERE Id = {product.Id} ";

                SqlCommand command = new SqlCommand(commandString, connection);

                connection.Open();

                var numberOfRows = command.ExecuteNonQuery();

                connection.Close();

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
                connection.Open();
                string commandString = $"SELECT * FROM [dbo].[Product];";

                SqlCommand command = new SqlCommand(commandString, connection);
                var reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var product = new Product();
                        product.Id = reader.GetInt32(0);
                        product.Name = reader.GetString(1);
                        product.Description = reader.GetString(2);
                        product.Weight = reader.GetDouble(3);
                        product.Height = reader.GetDouble(4);
                        product.Width = reader.GetDouble(5);
                        product.Length = reader.GetDouble(6);

                        yield return product;
                    }
                }

                reader.Close();
                connection.Close();
            }
        }
    }
}