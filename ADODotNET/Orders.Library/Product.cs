namespace Orders.Library
{
    public class Product: IEquatable<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public double Length { get; set; }

        public bool Equals(Product? other)
        {
            if (other == null) 
                return false;

            return Name == other.Name && Description == other.Description && Weight == other.Weight && Height == other.Height && Width == other.Width && Length == other.Length;
        }
    }
}