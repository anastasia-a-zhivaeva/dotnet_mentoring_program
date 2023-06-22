namespace HarryPotter
{
    internal class Cart
    {
        public Cart(List<Book> books)
        {
            Books = books;
        }

        public List<Book> Books { get; set; }

        internal double CalculateTotal()
        {
            if (Books == null)
                throw new ArgumentNullException(nameof(Books));

            var books = AggregateBooks();

            return CalculateTotal(books);
        }

        private Dictionary<int, CartBook> AggregateBooks()
        {
            var books = new Dictionary<int, CartBook>();

            foreach (var book in Books)
            {
                CartBook cartBook;
                if (books.TryGetValue(book.Order, out cartBook))
                    cartBook.Quantity += 1;
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
                return total;

            var discount = CalculateDiscount(books.Count);

            foreach (var pair in books)
            {
                var book = pair.Value;

                total += discount > 0 ? book.Price * (1 - discount) : book.Price;

                book.Quantity -= 1;
                if (book.Quantity == 0)
                    books.Remove(pair.Key);
            }

            return CalculateTotal(books, total);
        }

        private double CalculateDiscount(int count)
        {
            switch (count)
            {
                case 0: return 0;
                case 1: return 0;
                case 2: return 0.05;
                case 3: return 0.1;
                case 4: return 0.2;
                case 5: return 0.25;
                default: return 0.25;
            }
        }
    }
}