using System.Runtime.Serialization.Formatters.Binary;
using Serialization.Library;

#pragma warning disable SYSLIB0011

var employees = new List<Employee>
{
    new Employee("Will Smith"),
    new Employee("John Doe")
};

var department = new Department("Film Department", employees);

using (FileStream fs = new FileStream("data.dat", FileMode.Create))
{
    BinaryFormatter formatter = new BinaryFormatter();
    formatter.Serialize(fs, department);
}

using (FileStream fs = new FileStream("data.dat", FileMode.Open))
{
    BinaryFormatter formatter = new BinaryFormatter();
    Department deserializedDepartment = (Department)formatter.Deserialize(fs);
    Console.WriteLine(deserializedDepartment);
}