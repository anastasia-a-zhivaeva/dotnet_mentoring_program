using System.Runtime.Serialization;

namespace ISerializableImpl
{
    [Serializable]
    public class Employee: ISerializable
    {
        public string Name { get; set; }
        public int Salary { get; set; }

        public Employee() 
        {
            Name = string.Empty;
            Salary = 0;
        }

        public Employee(string name, int salary)
        {
            Name = name;
            Salary = salary;
        }

        protected Employee(SerializationInfo info, StreamingContext context)
        {
            Name = info.GetString("name") ?? string.Empty;
            Salary = info.GetInt32("salary");
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("name", Name);
            info.AddValue("salary", Salary);
        }

        public override string ToString() => $"{Name} - {Salary}";
    }
}
