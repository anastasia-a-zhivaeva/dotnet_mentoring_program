using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Serialization.Library
{
    [Serializable]
    public class Department
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
    }
}
