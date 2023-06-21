namespace Katas
{
    internal class CartBook : Book
    {
        public CartBook(Book book, int quantity) 
        {
            Name = book.Name;
            Order = book.Order;
            Price = book.Price;
            Quantity = quantity;
        }
        public int Quantity { get; set; }
    }
}