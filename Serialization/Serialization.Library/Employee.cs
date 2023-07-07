using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

#pragma warning disable SYSLIB0011

namespace Serialization.Library
{
    [Serializable]
    public class Employee: ICloneable
    {
        [XmlAttribute("name")]
        [JsonPropertyName("name")]  
        public string EmployeeName { get; set; }

        public Employee() 
        {
            EmployeeName = "Unknown employee";
        }
        public Employee(string employeeName)
        {
            EmployeeName = employeeName;
        }

        public override string ToString() => EmployeeName;
        public object Clone()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Position = 0;
                return (Employee)formatter.Deserialize(stream);
            }
        }
    }
}