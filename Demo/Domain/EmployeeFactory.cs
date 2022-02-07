namespace Demo.Domain
{
    public class EmployeeFactory
    {
        public static Employee Create(string name, double salary)
        {
            return new Employee(name, salary);
        }
    }
}
