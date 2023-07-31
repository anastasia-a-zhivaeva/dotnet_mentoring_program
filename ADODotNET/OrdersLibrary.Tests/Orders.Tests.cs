using Microsoft.Data.SqlClient;
using Orders.Library;

namespace OrdersLibrary.Tests
{
    public class OrdersTests: IDisposable
    {
        private readonly string _connectionString = "Data Source=EPUSPRIW009D;Initial Catalog=OrdersTestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private Orders _orders;
        private Products _products;
        private Product _product = TestData.product;
        private List<Product> _productsList = TestData.products;
        private Order _order = TestData.order;
        private List<Order> _ordersList = TestData.orders;

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

        [Fact]
        public void GetAll_ReturnsEmptyListIfNoOrdersWereAdded()
        {
            Assert.Empty(_orders.GetAll());
        }

        [Fact]
        public void GetAll_ReturnsAllOrdersIfNoFiltersUsed()
        {
            AddOrders();

            Assert.Equal(_ordersList.Count, _orders.GetAll().Count());
        }

        [Fact]
        public void GetAll_ReturnsOrdersFilteredByStatus()
        {
            AddOrders();
            var status = "Done";

            var orders = _orders.GetAll(status: "Done");

            Assert.Equal(1, orders.Count());
            Assert.True(orders.All(order => order.Status == status));
        }

        [Fact]
        public void GetAll_ReturnsOrdersFilteredByMonth()
        {
            AddOrders();
            var month = DateTime.Now.AddMonths(-1).Month;

            var orders = _orders.GetAll(month: month);

            Assert.Equal(2, orders.Count());
            Assert.True(orders.All(order => order.CreatedDate.Month == month));
        }

        [Fact]
        public void GetAll_ReturnsOrdersFilteredByYear()
        {
            AddOrders();
            var year = DateTime.Now.AddYears(-1).Year;

            var orders = _orders.GetAll(year: year);

            Assert.Equal(2, orders.Count());
            Assert.True(orders.All(order => order.CreatedDate.Year == year));
        }

        [Fact]
        public void GetAll_ReturnsOrdersFilteredByProduct()
        {
            var productId = AddOrders();

            var orders = _orders.GetAll(productId: productId);

            Assert.Equal(1, orders.Count());
            Assert.True(orders.All(order => order.ProductId == productId));
        }
        [Fact]
        public void GetAll_ReturnsOrdersFilteredByTwoOrMoreFilters()
        {
            AddOrders();
            var year = DateTime.Now.AddYears(-1).Year;
            var month = DateTime.Now.AddMonths(-1).Month;

            var orders = _orders.GetAll(year: year, month: month);

            Assert.Equal(1, orders.Count());
            Assert.True(orders.All(order => order.CreatedDate.Year == year && order.CreatedDate.Month == month));
        }

        private int AddOrders()
        {
            int firstProductId = 0;
            for (var i = 0; i < _productsList.Count; i++)
            {
                var productId = _products.Add(_productsList[i]);
                var order = new Order()
                {
                    Status = _ordersList[i].Status,
                    CreatedDate = _ordersList[i].CreatedDate,
                    UpdatedDate = _ordersList[i].UpdatedDate,
                    ProductId = productId,
                };
                _orders.Add(order);

                if (firstProductId == 0)
                    firstProductId = productId;
            }

            return firstProductId;
        }
    }
}
