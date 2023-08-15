using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using NorthwindAPIConsumer;

IConfigurationRoot config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
Settings? settings = config.GetRequiredSection("Settings").Get<Settings>();

using HttpClient client = new() { BaseAddress = new Uri(settings.NorthwindAPI) };
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(
    new MediaTypeWithQualityHeaderValue("application/json"));

await MakeHorthwindAPIRequests(client);

static async Task MakeHorthwindAPIRequests(HttpClient client)
{
    var categories = await client.GetFromJsonAsync<List<Category>>("Categories");

    Console.WriteLine("Categories: ");
    categories.ForEach(Console.WriteLine);

    var products = await client.GetFromJsonAsync<List<Product>>("Products");

    Console.WriteLine("First 10 products:");
    products.ForEach(Console.WriteLine);

    var filteredProducts = await client.GetFromJsonAsync<List<Product>>("Products?categoryId=1");

    Console.WriteLine("First 10 products of the first category:");
    filteredProducts.ForEach(Console.WriteLine);

    using HttpResponseMessage postResponse = await client.PostAsJsonAsync("Products", new Product() { ProductName = "My test product", CategoryId = 2, Discontinued = true });
    postResponse.EnsureSuccessStatusCode();

    var newProduct = await postResponse.Content.ReadFromJsonAsync<Product>();
    Console.WriteLine("New product:");
    Console.WriteLine(newProduct);

    using HttpResponseMessage putResponse = await client.PutAsJsonAsync($"Products/{newProduct.ProductId}", new Product() { ProductId = newProduct.ProductId, ProductName = "My updated test product", CategoryId = 3 });
    putResponse.EnsureSuccessStatusCode();

    var updatedProduct = await client.GetFromJsonAsync<Product>($"Products/{newProduct.ProductId}");
    Console.WriteLine("Updated product:");
    Console.WriteLine(updatedProduct);
    
    using HttpResponseMessage deleteResponse = await client.DeleteAsync($"Products/{newProduct.ProductId}");
    deleteResponse.EnsureSuccessStatusCode();
}