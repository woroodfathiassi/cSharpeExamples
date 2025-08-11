using ConsoleApp1.Models;

namespace ConsoleApp1.Services;

public class DepartmentService
{
    private readonly List<Department> _departments;

    public DepartmentService(List<Department> departments)
    {
        _departments = departments;
    }

    public void ShowAllDepartments()
    {
        if (!_departments.Any())
        {
            Console.WriteLine("No departments available.");
            return;
        }

        foreach (var dept in _departments)
        {
            Console.WriteLine($"- ID: {dept.Id}, Name: {dept.Name}");
        }
    }

    public void AddDepartment()
    {
        int id;
        while (true)
        {
            Console.Write("Enter department ID: ");
            if (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.WriteLine("Invalid ID. Please enter a number.");
                continue;
            }

            if (_departments.Any(d => d.Id == id))
            {
                Console.WriteLine("Department ID already exists. Try again.");
            }
            else
            {
                break;
            }
        }

        string name;
        while (true)
        {
            Console.Write("Enter department name: ");
            name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
            }
            else
            {
                break;
            }
        }

        _departments.Add(new Department(id, name));
        Console.WriteLine("Department added successfully.");
    }

    public void RemoveDepartment()
    {
        while (true)
        {
            Console.Write("Enter department ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID. Please enter a number.");
                continue;
            }

            var dept = _departments.FirstOrDefault(d => d.Id == id);
            if (dept != null)
            {
                _departments.Remove(dept);
                Console.WriteLine("Department removed.");
                break;
            }
            else
            {
                Console.WriteLine("Department not found. Try again.");
            }
        }
    }
    public void ShowEmployeesByDepartmentName()
    {
        while (true)
        {
            Console.Write("Enter department name: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                continue;
            }

            var dept = _departments.FirstOrDefault(d => d.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (dept == null)
            {
                Console.WriteLine("Department not found. Try again.");
                continue;
            }

            if (!dept.Employees.Any())
            {
                Console.WriteLine("No employees in this department.");
            }
            else
            {
                foreach (var emp in dept.Employees)
                {
                    Console.WriteLine($"- {emp.Name} (Age: {emp.Age})");
                }
            }

            break;
        }
    }
}