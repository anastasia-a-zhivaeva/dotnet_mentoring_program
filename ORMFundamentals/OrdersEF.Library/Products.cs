namespace OrdersEF.Library
{
    public class Products
    {
        private readonly OrdersContext _ordersContext;

        public Products(string connectionString)
        {
            _ordersContext = new OrdersContext(connectionString);
        }

        public Product Get(int id)
        {
            var product = _ordersContext.Products.Find(id);

            if (product == null)
                throw new KeyNotFoundException();
            return product;
        }

        public int Add(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            var addedProduct = _ordersContext.Products.Add(product);
            _ordersContext.SaveChanges();
            return addedProduct.Entity.Id;
        }

        public void Remove(int id)
        {
            var product = Get(id);
            _ordersContext.Products.Remove(product);
            _ordersContext.SaveChanges();
        }

        public void Update(Product product)
        {
            if (product == null)
                throw new ArgumentNullException(nameof(product));

            _ordersContext.Products.Update(product);
            _ordersContext.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
            return _ordersContext.Products.ToList();
        }
    }
}