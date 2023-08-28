using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

#pragma warning disable SYSLIB0011

namespace Serialization.Library
{
    [Serializable]
    public class Department: ICloneable
    {
        [XmlElement("name")]
        public string DepartmentName { get; set; }
        [XmlArray("CurrentEmployees")]
        [JsonPropertyName("CurrentEmployees")]
        public List<Employee> Employees { get; set; }

        public Department() 
        {
            DepartmentName = "Unknown department";
            Employees = new List<Employee>();
        }

        public Department(string departmentName,  List<Employee> employees)
        {
            DepartmentName = departmentName;
            Employees = employees;
        }

        public override string ToString()
        {
            var department = new StringBuilder(DepartmentName).AppendLine();
            foreach (var employee in Employees)
            {
                department.AppendLine(employee.ToString());
            }
            return department.ToString();
        }

        public object Clone()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, this);
                stream.Position = 0;
                return (Department)formatter.Deserialize(stream);
            }
        }
    }
}
