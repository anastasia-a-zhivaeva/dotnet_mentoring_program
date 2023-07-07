using Serialization.Library;

var employees = new List<Employee>
{
    new Employee("Will Smith"),
    new Employee("John Doe")
};

var department = new Department("Film Department", employees);

var employeeClone = (Employee)employees.First().Clone();

employeeClone.EmployeeName = "Will Black";
Console.WriteLine($"Original = {employees.First()}, Clone = {employeeClone}");

var departmentClone = (Department)department.Clone();
departmentClone.DepartmentName = "Music Department";
departmentClone.Employees.Add(new Employee("Katty Smith"));
departmentClone.Employees.First().EmployeeName = "Will White";
Console.WriteLine("Original =");
Console.WriteLine(department);
Console.WriteLine("Clone =");
Console.WriteLine(departmentClone);
