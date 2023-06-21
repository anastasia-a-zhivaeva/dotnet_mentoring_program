namespace Katas
{
	public class HarryPotterTests
	{

		private List<Book> books = new List<Book>()
		{
			new Book()
			{
				Name = "Harry Potter and the Philosopher's Stone",
				Order = 1,
				Price = 8,
			},
			new Book()
			{
				Name = "Harry Potter and the Chamber of Secrets",
				Order = 2,
				Price = 8,
			},
			new Book()
			{
				Name = "Harry Potter and the Prisoner of Azkaban",
				Order = 3,
				Price = 8,
			},
			new Book()
			{
				Name = "Harry Potter and the Goblet of Fire",
				Order = 4,
				Price = 8,
			},
			new Book()
			{
				Name = "Harry Potter and the Order of the Phoenix",
				Order = 5,
				Price = 8,
			},
		};

		[Fact]
		public void CalculateCartTotal_OneBookDoesNotHaveDiscount()
		{
			var cartBooks = new List<Book>()
			{
				books[0],
			};
			var cart = new Cart(cartBooks);

			double total = cart.CalculateTotal();

			Assert.Equal(8, total, 2);
		}

		[Fact]
		public void CalculateCartTotal_TwoDifferentBooksHave5PercentDiscount()
		{
			var cartBooks = new List<Book>()
			{
				books[0],
				books[1],
			};
			var cart = new Cart(cartBooks);

			double total = cart.CalculateTotal();

			Assert.Equal(15.2, total, 2);
        }

        [Fact]
        public void CalculateCartTotal_ThreeDifferentBooksHave10PercentDiscount()
        {
            var cartBooks = new List<Book>()
            {
                books[0],
                books[1],
				books[2],
            };
            var cart = new Cart(cartBooks);

            double total = cart.CalculateTotal();

            Assert.Equal(21.6, total, 2);
        }

        [Fact]
        public void CalculateCartTotal_FourDifferentBooksHave20PercentDiscount()
        {
            var cartBooks = new List<Book>()
            {
                books[0],
                books[1],
                books[2],
                books[3],
            };
            var cart = new Cart(cartBooks);

            double total = cart.CalculateTotal();

            Assert.Equal(25.6, total, 2);
        }

        [Fact]
        public void CalculateCartTotal_FiveDifferentBooksHave25PercentDiscount()
        {
            var cartBooks = new List<Book>()
            {
                books[0],
                books[1],
                books[2],
                books[3],
                books[4],
            };
            var cart = new Cart(cartBooks);

            double total = cart.CalculateTotal();

            Assert.Equal(30, total, 2);
        }

        [Fact]
        public void CalculateCartTotal_ManyDifferentAndSameBooksHaveDiscount()
        {
            var cartBooks = new List<Book>()
            {
                books[0],
                books[0],
                books[1],
                books[1],
                books[2],
                books[2],
                books[3],
                books[4],
            };
            var cart = new Cart(cartBooks);

            double total = cart.CalculateTotal();

            Assert.Equal(51.6, total, 2);
        }
    }
}