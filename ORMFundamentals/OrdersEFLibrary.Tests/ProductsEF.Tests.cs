using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using OrdersEF.Library;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace OrdersEFLibrary.Tests
{
    public class ProductsTests : IDisposable
    {
        private readonly string _connectionString = "Data Source=EPUSPRIW009D;Initial Catalog=OrdersDBORMTest;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private Products _products;
        private Product _product = (Product)TestData.product.Clone();
        private List<Product> _productsList = TestData.products.ConvertAll(product => (Product)product.Clone());

        public ProductsTests()
        {
            _products = new Products(_connectionString);
        }

        public void Dispose()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string commandString = "DELETE FROM [dbo].[Product] DBCC CHECKIDENT ('dbo.Product', RESEED, 0)";
                SqlCommand command = new SqlCommand(commandString, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        [Fact]
        public void Add_ThrowIfProductIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _products.Add(null));
        }

        [Fact]
        public void Add_AddsProductAndReturnsItsId()
        {
            var id = _products.Add(_product);

            var result = _products.Get(id);
            Assert.Equal(id, result.Id);
            Assert.Equal(_product.Name, result.Name);
            Assert.Equal(_product.Description, result.Description);
            Assert.Equal(_product.Weight, result.Weight);
            Assert.Equal(_product.Width, result.Width);
            Assert.Equal(_product.Height, result.Height);
            Assert.Equal(_product.Length, result.Length);
        }

        [Fact]
        public void Get_ThrowIfProductDoesNotExist()
        {
            Assert.Throws<KeyNotFoundException>(() => _products.Get(100000));
        }

        [Fact]
        public void Get_ReturnsProductIfExists()
        {
            var id = _products.Add(_product);

            var result = _products.Get(id);

            Assert.Equal(id, result.Id);
            Assert.Equal(_product.Name, result.Name);
            Assert.Equal(_product.Description, result.Description);
            Assert.Equal(_product.Weight, result.Weight);
            Assert.Equal(_product.Width, result.Width);
            Assert.Equal(_product.Height, result.Height);
            Assert.Equal(_product.Length, result.Length);
        }

        [Fact]
        public void Remove_ThrowIfProductDoesNotExist()
        {
            Assert.Throws<KeyNotFoundException>(() => _products.Remove(100000));
        }

        [Fact]
        public void Remove_DoesNotReturnProductAfterRemoved()
        {
            var id = _products.Add(_product);

            _products.Remove(id);

            Assert.Throws<KeyNotFoundException>(() => _products.Get(id));
        }


        [Fact]
        public void Update_ThrowIfProductIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _products.Update(null));
        }

        [Fact]
        public void Update_ThrowIfProductDoesNotExist()
        {
            var product = new Product
            {
                Id = 100000,
                Name = "iPhone 14",
                Description = "A total powerhouse.",
                Weight = 6.07,
                Height = 5.78,
                Width = 2.82,
                Length = 0.31
            };

            Assert.Throws<DbUpdateConcurrencyException>(() => _products.Update(product));
        }

        [Fact]
        public void Update_ReturnsUpdatedProduct()
        {
            var originalName = _product.Name;
            var id = _products.Add(_product);
            _product.Id = id;
            _product.Name = "iPhone 14 Pro";

            _products.Update(_product);

            var result = _products.Get(id);
            Assert.Equal(id, result.Id);
            Assert.NotEqual(originalName, result.Name);
            Assert.Equal(_product.Name, result.Name);
            Assert.Equal(_product.Description, result.Description);
            Assert.Equal(_product.Weight, result.Weight);
            Assert.Equal(_product.Width, result.Width);
            Assert.Equal(_product.Height, result.Height);
            Assert.Equal(_product.Length, result.Length);
        }


        [Fact]
        public void GetAll_ReturnsEmptyListIfNoProductsWereAdded()
        {
            Assert.Empty(_products.GetAll());
        }


        [Fact]
        public void GetAll_ReturnsAddedProducts()
        {
            foreach (var product in _productsList)
            {
                _products.Add(product);
            }

            var result = _products.GetAll();

            Assert.True(Enumerable.SequenceEqual(_productsList, result.OrderBy(p => p.Id)));
        }

    }
}