namespace LinQ.Problems1;

public class Linq1
{
    public static void Question1()
    {
        Console.WriteLine("\nQ1: ");
        var products = new List<string> { "Laptop", "Mouse", "Keyboard", "Monitor", "Speaker" };

        //Write a LINQ query to return only the products whose names start with the letter "M".
        var mProducts = products.Where(p => p.StartsWith("M"));
        foreach (String p in mProducts)
            Console.WriteLine(p);
    }

    public static void Question2()
    {
        Console.WriteLine("\nQ2: ");
        var orders = new List<Order>
        {
            new() { OrderId = 1, CustomerName = "Ali", Amount = 250 },
            new() { OrderId = 2, CustomerName = "Ahmad", Amount = 150 },
            new() { OrderId = 3, CustomerName = "Ali", Amount = 300 },
            new() { OrderId = 3, CustomerName = "Haya", Amount = 300 }
        };

        //Write a LINQ query to group orders by CustomerName and calculate the total amount spent by each customer.
        var totals = orders
            .GroupBy(o => o.CustomerName)
            .Select(g => new
            {
                CustomerName = g.Key,
                TotalAmount = g.Sum(o => o.Amount)
            });

        foreach (var item in totals)
            Console.WriteLine($"{item.CustomerName} - {item.TotalAmount}");
    }

    public static void Question3()
    {
        var employees = new List<Employee>
        {
            new() { Id = 1, Name = "Nidal", Salary = 3000 },
            new() { Id = 2, Name = "Sara", Salary = 4000 },
            new() { Id = 3, Name = "Areen", Salary = 3500 },
            new() { Id = 4, Name = "Abdullah", Salary = 3000 },
            new() { Id = 5, Name = "Ehab", Salary = 1000 } ,
            new() { Id = 6, Name = "Afaf", Salary = 3500 },
            new() { Id = 7, Name = "Worood", Salary = 3500 },
        };

        // 1. Group employees by Salary.
        Console.WriteLine("\nQ3.1: ");
        var q1 = employees.GroupBy(o => o.Salary);
        foreach (var str in q1) Console.WriteLine(str.Key);

        // 2. Within each group, order employees by Name alphabetically.
        Console.WriteLine("\nQ3.2: ");
        var q2 = q1.Select(
                g => new
                {
                    salary = g.Key,
                    names = string.Join(", ",
                    g.OrderBy(e => e.Name)
                    .Select(e => e.Name))
                });

        foreach (var item in q2) Console.WriteLine($"{item.salary} - {item.names}");

        // 3. Select only those groups where the number of employees in that salary group is greater than 2.
        Console.WriteLine("\nQ3.3: ");
        var q3 = q1.Where(c => c.Count() > 2).Select(g => new { salary = g.Key, count = g.Count() });
        foreach (var item in q3) Console.WriteLine($"{item.salary} - {item.count}");

        // 4. Return the result as a list of objects with:
        // o Salary
        // o EmployeeNames (comma-separated string of names in that group)
        // Example output format:
        // [
        //    { Salary = 3500, EmployeeNames = " Afaf, Areen, Worood" }
        // ]
        Console.WriteLine("\nQ3.4: ");
        var q4 = q1.Select(
            (g, names) => new
            {
                salary = g.Key,
                names = string
                        .Join(", ", g
                            .OrderBy(e => e.Name)
                            .Select(e => e.Name))
            });
        foreach (var obj in q4) Console.WriteLine("{0}", obj);
    }

    public static void Question4()
    {
        Console.WriteLine("\nQ4: ");
        var students = new List<Student>
        {
            new() { Id = 1, Name = "Ali" },
            new() { Id = 3, Name = "Ahmad" },
            new() { Id = 2, Name = "Sara" },
        };

            var grades = new List<Grade>
        {
            new() { StudentId = 1, Score = 85 },
            new() { StudentId = 3, Score = 4 },
            new() { StudentId = 2, Score = 92 },
        };
        // Write a LINQ join query to return each student's name along with their score.
        var result = from student in students
                     join grade in grades on student.Id equals grade.StudentId
                     select new
                     {
                         studentName = student.Name,
                         studentGrade = grade.Score
                     };

        foreach (var item in result) Console.WriteLine($"{item.studentName} : {item.studentGrade}");
    }
}


#region Classes
public class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Grade
{
    public int StudentId { get; set; }
    public double Score { get; set; }
}

public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Salary { get; set; }
}

public class Order
{
    public int OrderId { get; set; }
    public int Amount { get; set; }
    public string CustomerName { get; set; }
}
#endregion