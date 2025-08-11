using ConsoleApp1.Models;

namespace ConsoleApp1.Services;

public class EmployeeService
{
    private readonly List<Department> _departments;

    public EmployeeService(List<Department> departments)
    {
        _departments = departments;
    }

    public void AddEmployee()
    {
        Console.Write("Enter employee name: ");
        string name = Console.ReadLine();

        int age;
        while (true)
        {
            Console.Write("Enter employee age: ");
            if (int.TryParse(Console.ReadLine(), out age) && age > 0)
                break;
            Console.WriteLine("Invalid age. Please enter a valid number greater than 0.");
        }

        int depId;
        Department department;
        while (true)
        {
            Console.Write("Enter department ID: ");
            if (!int.TryParse(Console.ReadLine(), out depId))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            department = _departments.FirstOrDefault(d => d.Id == depId);
            if (department == null)
            {
                Console.WriteLine("Department not found. Please enter a valid department ID.");
            }
            else
            {
                break;
            }
        }

        department.Employees.Add(new Employee(name, age, depId));
        Console.WriteLine("Employee added successfully.");
    }
    public void RemoveEmployee()
    {
        while (true)
        {
            Console.Write("Enter employee name to remove: ");
            string name = Console.ReadLine();

            bool found = false;

            foreach (var dept in _departments)
            {
                var emp = dept.Employees.FirstOrDefault(e => e.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                if (emp != null)
                {
                    dept.Employees.Remove(emp);
                    Console.WriteLine("Employee removed.");
                    found = true;
                    break;
                }
            }

            if (found)
                break;
            else
                Console.WriteLine("Employee not found. Try again.");
        }
    }

    public void SearchEmployeeByName()
    {
        while (true)
        {
            Console.Write("Enter name to search: ");
            string name = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Name cannot be empty.");
                continue;
            }

            var matches = _departments
                .SelectMany(d => d.Employees, (d, e) => new { Department = d.Name, Employee = e })
                .Where(x => x.Employee.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (matches.Any())
            {
                foreach (var match in matches)
                {
                    Console.WriteLine($"- {match.Employee.Name} (Age: {match.Employee.Age}), Department: {match.Department}");
                }
                break;
            }
            else
            {
                Console.WriteLine("No employees found with that name. Try again.");
            }
        }
    }

    public void ShowAllEmployees()
    {
        bool found = false;
        foreach (var dept in _departments)
        {
            foreach (var emp in dept.Employees)
            {
                Console.WriteLine($"- {emp.Name} (Age: {emp.Age}) - Department: {dept.Name}");
                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("No employees available.");
        }
    }
}
