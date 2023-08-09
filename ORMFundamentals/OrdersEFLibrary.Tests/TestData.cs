using OrdersEF.Library;

namespace OrdersEFLibrary.Tests
{
    internal class TestData
    {
        public static readonly Product product = new Product
        {
            Name = "iPhone 14",
            Description = "A total powerhouse.",
            Weight = 6.07,
            Height = 5.78,
            Width = 2.82,
            Length = 0.31
        };

        public static readonly List<Product> products = new List<Product>()
        {
            new Product
            {
                Name = "iPhone 14",
                Description = "A total powerhouse.",
                Weight = 6.07,
                Height = 5.78,
                Width = 2.82,
                Length = 0.31
            },
            new Product
            {
                Name = "iPhone 14 Pro",
                Description = "The ultimate iPhone.",
                Weight = 7.27,
                Height = 5.81,
                Width = 2.81,
                Length = 0.31
            },
            new Product
            {
                Name = "iPhone 13",
                Description = "As amazing as ever.",
                Weight = 6.14,
                Height = 5.78,
                Width = 2.82,
                Length = 0.3
            },
            new Product
            {
                Name = "iPhone SE",
                Description = "Serious power. Serious value.",
                Weight = 5.09,
                Height = 5.45,
                Width = 2.65,
                Length = 0.29
            },
        };

        public static readonly Order order = new Order
        {
            Status = "NotStarted",
            CreatedDate = DateTime.Now.AddDays(-1),
            UpdatedDate = DateTime.Now,
        };

        public static readonly List<Order> orders = new List<Order>()
        {
            new Order
            {
                Status = "NotStarted",
                CreatedDate = DateTime.Now.AddDays(-1),
                UpdatedDate = DateTime.Now,
            },
            new Order
            {
                Status = "Loading",
                CreatedDate = DateTime.Now.AddMonths(-1),
                UpdatedDate = DateTime.Now,
            },
            new Order
            {
                Status = "InProgress",
                CreatedDate= DateTime.Now.AddYears(-1),
                UpdatedDate = DateTime.Now,
            },
            new Order
            {
                Status = "Done",
                CreatedDate = DateTime.Now.AddYears(-1).AddMonths(-1),
                UpdatedDate = DateTime.Now,
            }
        };
    }
}
