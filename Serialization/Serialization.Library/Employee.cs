using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Serialization.Library
{
    [Serializable]
    public class Employee
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
    }
}