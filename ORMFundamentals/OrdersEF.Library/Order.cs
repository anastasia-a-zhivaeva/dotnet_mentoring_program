namespace OrdersEF.Library;

public partial class Order: ICloneable
{
    public int Id { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public object Clone() => new Order
    {
        Id = Id,
        Status = Status,
        CreatedDate = CreatedDate,
        UpdatedDate = UpdatedDate,
        ProductId = ProductId,
        Product = Product,
    };
}
