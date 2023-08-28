using System.Runtime.Serialization.Formatters.Binary;
using ISerializableImpl;

#pragma warning disable SYSLIB0011

var employee = new Employee("Will Smith", 10000000);

using (FileStream fs = new FileStream("data.dat", FileMode.Create))
{
    BinaryFormatter formatter = new BinaryFormatter();
    formatter.Serialize(fs, employee);
}

using (FileStream fs = new FileStream("data.dat", FileMode.Open))
{
    BinaryFormatter formatter = new BinaryFormatter();
    Employee deserializedEmployee = (Employee)formatter.Deserialize(fs);
    Console.WriteLine(deserializedEmployee);
}
