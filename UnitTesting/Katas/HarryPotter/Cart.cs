﻿namespace Katas
{
    internal class Cart
    {
        private static readonly Dictionary<int, double> _discounts = new Dictionary<int, double>()
        {
            { 0, 0 },
            { 1, 0 },
            { 2, 0.05 },
            { 3, 0.01 },
            { 4, 0.2 },
            { 5, 0.25 },
        };

        public Cart()
        {
        }

        public List<Book> Books { get; set; }

        internal static double CalculateTotal(Cart cart)
        {
            double total = 0;
            Dictionary<int, (Book book, int quantity)> books = new Dictionary<int, (Book book, int quantity)>();

            foreach (Book cartBook in cart.Books)
            {
                (Book book, int quantity) value;
                if (books.TryGetValue(cartBook.Order, out value))
                {
                    value.quantity += 1;
                }
                else
                {
                    books.Add(cartBook.Order, (cartBook, 1));
                }
            }

            double discount = CalculateDiscount(books.Count);

            foreach (KeyValuePair<int, (Book book, int quantity)> pair in books)
            {
                var book = pair.Value.book;
                var quantity = pair.Value.quantity;

                total += discount  >  0 ? book.Price * (1 - discount) : book.Price;

                var quantityLeft = quantity - 1;
                if (quantityLeft > 0)
                {
                    total += book.Price * quantityLeft;
                }
            }

            return total;
        }

        private static double CalculateDiscount(int count)
        {
            double discount;
            _discounts.TryGetValue(count, out discount);
            return discount;
        }
    }
}