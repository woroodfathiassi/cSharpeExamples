namespace ConsoleApp1.Models;

public class Department
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Employee> Employees { get; }

    public Department(int id, string name)
    {
        Id = id;
        Name = name;
        Employees = [];
    }
}
