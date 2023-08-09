using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrdersEF.Library;

namespace OrdersEFLibrary.Tests
{
    public class OrdersTests : IDisposable
    {
        private readonly string _connectionString = "Data Source=EPUSPRIW009D;Initial Catalog=OrdersDBORMTest;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private Orders _orders;
        private Products _products;
        private Product _product = (Product)TestData.product.Clone();
        private List<Product> _productsList = TestData.products.ConvertAll(product => (Product)product.Clone());
        private Order _order = (Order)TestData.order.Clone();
        private List<Order> _ordersList = TestData.orders.ConvertAll(order => (Order)order.Clone());

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
            Assert.Equal(DateTime.Parse(order.CreatedDate.ToString("yyyy-MM-dd hh:mm:ss.000")), DateTime.Parse(result.CreatedDate.ToString("yyyy-MM-dd hh:mm:ss.000")));
            Assert.Equal(DateTime.Parse(order.UpdatedDate.ToString("yyyy-MM-dd hh:mm:ss.000")), DateTime.Parse(result.UpdatedDate.ToString("yyyy-MM-dd hh:mm:ss.000")));
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
            Assert.Equal(DateTime.Parse(order.CreatedDate.ToString("yyyy-MM-dd hh:mm:ss.000")), DateTime.Parse(result.CreatedDate.ToString("yyyy-MM-dd hh:mm:ss.000")));
            Assert.Equal(DateTime.Parse(order.UpdatedDate.ToString("yyyy-MM-dd hh:mm:ss.000")), DateTime.Parse(result.UpdatedDate.ToString("yyyy-MM-dd hh:mm:ss.000")));
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

            Assert.Throws<DbUpdateConcurrencyException>(() => _orders.Update(order));
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
            var originalStatus = order.Status;
            order.Status = "InProgress";

            _orders.Update(order);

            var result = _orders.Get(id);
            Assert.Equal(id, result.Id);
            Assert.NotEqual(originalStatus, result.Status);
            Assert.Equal(order.Status, result.Status);
            Assert.Equal(DateTime.Parse(order.CreatedDate.ToString("yyyy-MM-dd hh:mm:ss.000")), DateTime.Parse(result.CreatedDate.ToString("yyyy-MM-dd hh:mm:ss.000")));
            Assert.Equal(DateTime.Parse(order.UpdatedDate.ToString("yyyy-MM-dd hh:mm:ss.000")), DateTime.Parse(result.UpdatedDate.ToString("yyyy-MM-dd hh:mm:ss.000")));
            Assert.Equal(order.ProductId, result.ProductId);
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

        [Fact]
        public void BulkDelete_DoesNotThrowIfNoOrdersToDelete()
        {
            AddOrders();
            var month = DateTime.Now.AddMonths(-2).Month;

            _orders.BulkDelete(month: month);

            Assert.Equal(_ordersList.Count, _orders.GetAll().Count());
        }

        [Fact]
        public void BulkDelete_DeletesOrdersFilteredByStatus()
        {
            AddOrders();
            var status = "Done";

            _orders.BulkDelete(status: status);

            Assert.Equal(_ordersList.Count - 1, _orders.GetAll().Count());
        }
    }
}