using System.Text.Json.Serialization;

namespace NorthwindAPIConsumer;

public class Category
{
    [JsonPropertyName("categoryId")]
    public int CategoryId { get; set; }

    [JsonPropertyName("categoryName")]
    public string CategoryName { get; set; } = null!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("picture")]
    public byte[]? Picture { get; set; }

    override public string ToString()
    {
        return $"{CategoryId} - {CategoryName}";
    }
}
