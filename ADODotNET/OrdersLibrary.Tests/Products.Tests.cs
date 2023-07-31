using Microsoft.Data.SqlClient;
using Orders.Library;

[assembly: CollectionBehavior(DisableTestParallelization = true)]
namespace OrdersLibrary.Tests
{
    public class ProductsTests: IDisposable
    {
        private readonly string _connectionString = "Data Source=EPUSPRIW009D;Initial Catalog=OrdersTestDB;Integrated Security=True;Connect Timeout=60;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        private Products _products;
        private Product _product = TestData.product;
        private List<Product> _productsList = TestData.products;

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

            Assert.Equal(id,  result.Id);
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

            Assert.Throws<KeyNotFoundException>(() => _products.Update(product));
        }

        [Fact]
        public void Update_ReturnsUpdatedProduct()
        {
            var id = _products.Add(_product);
            var productToUpdate = new Product
            {
                Id = id,
                Name = "iPhone 14 Pro",
                Description = _product.Description,
                Weight = _product.Weight,
                Height = _product.Height,
                Width = _product.Width,
                Length = _product.Length
            };

            _products.Update(productToUpdate);

            var result = _products.Get(id);
            Assert.Equal(id, result.Id);
            Assert.NotEqual(_product.Name, result.Name);
            Assert.Equal(productToUpdate.Name, result.Name);
            Assert.Equal(productToUpdate.Description, result.Description);
            Assert.Equal(productToUpdate.Weight, result.Weight);
            Assert.Equal(productToUpdate.Width, result.Width);
            Assert.Equal(productToUpdate.Height, result.Height);
            Assert.Equal(productToUpdate.Length, result.Length);
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