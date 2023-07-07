using Serialization.Library;
using System.Text.Json;

var employees = new List<Employee>
{
    new Employee("Will Smith"),
    new Employee("John Doe")
};

var department = new Department("Film Department", employees);

using (var writer = File.Create("data.json"))
{
    await JsonSerializer.SerializeAsync(writer, department, new JsonSerializerOptions { WriteIndented = true });
    await writer.DisposeAsync();
}

using (var reader = File.OpenRead("data.json"))
{
    Department deserializedDepartment = await JsonSerializer.DeserializeAsync<Department>(reader);
    Console.WriteLine(deserializedDepartment);
}