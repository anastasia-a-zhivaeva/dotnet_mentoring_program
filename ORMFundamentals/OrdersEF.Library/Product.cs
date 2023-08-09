namespace OrdersEF.Library;

public partial class Product: ICloneable
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public double? Weight { get; set; }

    public double? Height { get; set; }

    public double? Width { get; set; }

    public double? Length { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public object Clone() => new Product
    {
        Id = Id,
        Name = Name,
        Description = Description,
        Weight = Weight,
        Height = Height,
        Width = Width,
        Length = Length,
        Orders = Orders
    };
}
