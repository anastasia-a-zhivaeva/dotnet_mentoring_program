namespace OrdersLibrary
{
    public class Order
    {
        public string Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int ProductId { get; set; }
        public int Id { get; set; }
    }
}