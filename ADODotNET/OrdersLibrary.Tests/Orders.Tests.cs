using Microsoft.Data.SqlClient;
using Orders.Library;

namespace OrdersLibrary.Tests
{
    public class OrdersTests: IDisposable
    {
        private readonly string _connectionString = "Data Source=EPUSPRIW009D;Initial Catalog=OrdersTestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private Orders _orders;
        private Products _products;
        private Product _product = new Product
        {
            Name = "iPhone 14",
            Description = "A total powerhouse.",
            Weight = 6.07,
            Height = 5.78,
            Width = 2.82,
            Length = 0.31
        };
        private Order _order = new Order
        {
            Status = "NotStarted",
            CreatedDate = DateTime.Now.AddDays(-1),
            UpdatedDate = DateTime.Now,
        };

        public OrdersTests()
        {
            _orders = new Orders(_connectionString);
            _products = new Products(_connectionString);
        }

        public void Dispose()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string orderCommandString = "DELETE FROM [dbo].[Order] DBCC CHECKIDENT ('dbo.Order', RESEED, 0)";
                SqlCommand orderCommand = new SqlCommand(orderCommandString, connection);
                orderCommand.ExecuteNonQuery();

                string productCommandString = "DELETE FROM [dbo].[Product] DBCC CHECKIDENT ('dbo.Product', RESEED, 0)";
                SqlCommand productCommand = new SqlCommand(productCommandString, connection);
                productCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

        [Fact]
        public void Add_ThrowIfOrderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _orders.Add(null));
        }

        [Fact]
        public void Add_AddsOrderAndReturnsItsId()
        {
            var productId = _products.Add(_product);
            var order = new Order
            {
                Status = _order.Status,
                CreatedDate = _order.CreatedDate,
                UpdatedDate = _order.UpdatedDate,
                ProductId = productId,
            };

            var id = _orders.Add(order);

            var result = _orders.Get(id);
            Assert.Equal(id, result.Id);
            Assert.Equal(order.Status, result.Status);
            Assert.Equal(order.CreatedDate, result.CreatedDate);
            Assert.Equal(order.UpdatedDate, result.UpdatedDate);
            Assert.Equal(order.ProductId, result.ProductId);
        }

        [Fact]
        public void Get_ThrowIfOrderDoesNotExist()
        {
            Assert.Throws<KeyNotFoundException>(() => _orders.Get(10000));
        }

        [Fact]
        public void Get_ReturnsOrderIfExists()
        {
            var productId = _products.Add(_product);
            var order = new Order
            {
                Status = _order.Status,
                CreatedDate = _order.CreatedDate,
                UpdatedDate = _order.UpdatedDate,
                ProductId = productId,
            };
            var id = _orders.Add(order);

            var result = _orders.Get(id);

            Assert.Equal(id, result.Id);
            Assert.Equal(order.Status, result.Status);
            Assert.Equal(order.CreatedDate, result.CreatedDate);
            Assert.Equal(order.UpdatedDate, result.UpdatedDate);
            Assert.Equal(order.ProductId, result.ProductId);
        }

        [Fact]
        public void Remove_ThrowIfOrderDoesNotExist()
        {
            Assert.Throws<KeyNotFoundException>(() => _orders.Remove(100000));
        }

        [Fact]
        public void Remove_DoesNotReturnOrderAfterRemoved()
        {
            var productId = _products.Add(_product);
            var order = new Order
            {
                Status = _order.Status,
                CreatedDate = _order.CreatedDate,
                UpdatedDate = _order.UpdatedDate,
                ProductId = productId,
            };
            var id = _orders.Add(order);

            _orders.Remove(id);

            Assert.Throws<KeyNotFoundException>(() => _orders.Get(id));
        }

        [Fact]
        public void Update_ThrowIfOrderIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _orders.Update(null));
        }

        [Fact]
        public void Update_ThrowIfOrderDoesNotExist()
        {
            var order = new Order
            {
                Id = 10000,
                Status = _order.Status,
                CreatedDate = _order.CreatedDate,
                UpdatedDate = _order.UpdatedDate,
                ProductId = 1,
            };

            Assert.Throws<KeyNotFoundException>(() => _orders.Update(order));
        }

        [Fact]
        public void Update_ReturnsUpdatedOrder()
        {
            var productId = _products.Add(_product);
            var order = new Order
            {
                Status = _order.Status,
                CreatedDate = _order.CreatedDate,
                UpdatedDate = _order.UpdatedDate,
                ProductId = productId,
            };
            var id = _orders.Add(order);
            var orderToUpdate = new Order
            {
                Id = id,
                Status = "InProgress",
                CreatedDate = _order.CreatedDate,
                UpdatedDate = DateTime.Now,
                ProductId = productId,
            };

            _orders.Update(orderToUpdate);

            var result = _orders.Get(id);
            Assert.Equal(id, result.Id);
            Assert.NotEqual(order.Status, result.Status);
            Assert.Equal(orderToUpdate.Status, result.Status);
            Assert.Equal(orderToUpdate.CreatedDate, result.CreatedDate);
            Assert.Equal(orderToUpdate.UpdatedDate, result.UpdatedDate);
            Assert.Equal(orderToUpdate.ProductId, result.ProductId);
        }
    }
}
