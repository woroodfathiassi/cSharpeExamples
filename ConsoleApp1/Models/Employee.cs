namespace ConsoleApp1.Models;

public class Employee
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int DepartmentId { get; set; }

    public Employee(string name, int age, int depId)
    {
        Name = name;
        Age = age;
        DepartmentId = depId;
    }
}
