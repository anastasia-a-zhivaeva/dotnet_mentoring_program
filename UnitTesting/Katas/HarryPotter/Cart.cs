namespace Katas
{
    internal class Cart
    {
        private readonly Dictionary<int, double> _discounts = new Dictionary<int, double>()
        {
            { 0, 0 },
            { 1, 0 },
            { 2, 0.05 },
            { 3, 0.1 },
            { 4, 0.2 },
            { 5, 0.25 },
        };

        public Cart(List<Book> books)
        {
            Books = books;
        }

        public List<Book> Books { get; set; }

        internal double CalculateTotal()
        {
            Dictionary<int, CartBook> books = AggregateBooks();

            return CalculateTotal(books);
        }

        private Dictionary<int, CartBook> AggregateBooks()
        {
            Dictionary<int, CartBook> books = new Dictionary<int, CartBook>();

            foreach (Book book in Books)
            {
                CartBook cartBook;
                if (books.TryGetValue(book.Order, out cartBook))
                {
                    cartBook.Quantity += 1;
                }
                else
                {
                    books.Add(book.Order, new CartBook(book, 1));
                }
            }

            return books;
        }

        private double CalculateTotal(Dictionary<int, CartBook> books, double total = 0)
        {
            if (books.Count == 0)
            {
                return total;
            }

            double discount = CalculateDiscount(books.Count);

            foreach (KeyValuePair<int, CartBook> pair in books)
            {
                var book = pair.Value;

                total += discount > 0 ? book.Price * (1 - discount) : book.Price;

                book.Quantity -= 1;
                if (book.Quantity == 0)
                {
                    books.Remove(pair.Key);
                }
            }

            return CalculateTotal(books, total);
        }

        private double CalculateDiscount(int count)
        {
            double discount;
            _discounts.TryGetValue(count, out discount);
            return discount;
        }
    }
}