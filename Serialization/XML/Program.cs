using System.Xml.Serialization;
using Serialization.Library;

var employees = new List<Employee>
{
    new Employee("Will Smith"),
    new Employee("John Doe")
};

var department = new Department("Film Department", employees);

var xmlSerializer = new XmlSerializer(typeof(Department));
using (var writer = new StreamWriter("data.xml"))
{
    xmlSerializer.Serialize(writer, department);
}

using(var reader = new StreamReader("data.xml"))
{
    Department deserializedDepartment = (Department) xmlSerializer.Deserialize(reader);
    Console.WriteLine(deserializedDepartment);
}