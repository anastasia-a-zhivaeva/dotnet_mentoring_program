namespace Katas
{
    internal class Cart
    {
        public Cart()
        {
        }

        public List<Book> Books { get; set; }

        internal static double CalculateTotal(Cart cart)
        {
            double total = 0;

            foreach (Book book in cart.Books)
            {
                total += book.Price;
            }

            return total;
        }
    }
}