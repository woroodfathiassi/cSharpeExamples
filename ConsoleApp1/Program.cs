
//using ConsoleApp1.Models;
//using ConsoleApp1.Services;

//List<Department> departments = [];

//var employeeService = new EmployeeService(departments);
//var departmentService = new DepartmentService(departments);

//while (true)
//{
//    ShowMainMenu();
//    string mainChoice = Console.ReadLine();

//    switch (mainChoice)
//    {
//        case "1":
//            ShowEmployeeMenu(employeeService);
//            break;
//        case "2":
//            ShowDepartmentMenu(departmentService);
//            break;
//        case "3":
//            Console.WriteLine("Exiting program...");
//            return;
//        default:
//            Console.WriteLine("Invalid input. Press Enter to try again.");
//            Console.ReadLine();
//            break;
//    }
//}

//void ShowMainMenu()
//{
//    Console.Clear();
//    Console.WriteLine("===== Employee & Department Management System =====");
//    Console.WriteLine("1. Manage Employees");
//    Console.WriteLine("2. Manage Departments");
//    Console.WriteLine("3. Exit");
//    Console.Write("Enter your choice: ");
//}

//void ShowEmployeeMenu(EmployeeService employeeService)
//{
//    while (true)
//    {
//        Console.Clear();
//        Console.WriteLine("===== Employee Management =====");
//        Console.WriteLine("1. Add Employee");
//        Console.WriteLine("2. Remove Employee");
//        Console.WriteLine("3. Search Employee by Name");
//        Console.WriteLine("4. Show All Employees");
//        Console.WriteLine("5. Back to Main Menu");
//        Console.Write("Enter your choice: ");

//        string choice = Console.ReadLine();
//        switch (choice)
//        {
//            case "1": employeeService.AddEmployee(); break;
//            case "2": employeeService.RemoveEmployee(); break;
//            case "3": employeeService.SearchEmployeeByName(); break;
//            case "4": employeeService.ShowAllEmployees(); break;
//            case "5": return;
//            default:
//                Console.WriteLine("Invalid choice. Press Enter to try again.");
//                Console.ReadLine();
//                break;
//        }

//        Console.WriteLine("Press Enter to continue...");
//        Console.ReadLine();
//    }
//}

//void ShowDepartmentMenu(DepartmentService departmentService)
//{
//    while (true)
//    {
//        Console.Clear();
//        Console.WriteLine("===== Department Management =====");
//        Console.WriteLine("1. Add Department");
//        Console.WriteLine("2. Remove Department");
//        Console.WriteLine("3. Show Employees by Department Name");
//        Console.WriteLine("4. Show All Departments");
//        Console.WriteLine("5. Back to Main Menu");
//        Console.Write("Enter your choice: ");

//        string choice = Console.ReadLine();
//        switch (choice)
//        {
//            case "1": departmentService.AddDepartment(); break;
//            case "2": departmentService.RemoveDepartment(); break;
//            case "3": departmentService.ShowEmployeesByDepartmentName(); break;
//            case "4": departmentService.ShowAllDepartments(); break;
//            case "5": return;
//            default:
//                Console.WriteLine("Invalid choice. Press Enter to try again.");
//                Console.ReadLine();
//                break;
//        }

//        Console.WriteLine("Press Enter to continue...");
//        Console.ReadLine();
//    }
//}


#region Practice
//string content = await File.ReadAllTextAsync("C:\\Users\\Admin\\Desktop\\University\\Traning\\Universe\\data.txt");
//Console.WriteLine(content);

//Console.WriteLine("Worood Assi");


//static async Task<int> GetThreadIdAsync()
//{
//    int before = Environment.CurrentManagedThreadId;
//    await Task.Delay(1000).ConfigureAwait(false);
//    int after = Environment.CurrentManagedThreadId;

//    Console.WriteLine($"Before: {before}, After: {after}");
//    return after;
//}

//await GetThreadIdAsync();

//int[] numbers = [10,20,30];
//ref int second = ref numbers[1];
//second = 50;
//Console.WriteLine(numbers[1]);



// ref int FindNumber(ref int[] numbers, int target)
//{
//    for (int i = 0; i < numbers.Length; i++)
//    {
//        if (numbers[i] == target)
//            return ref numbers[i];  // return reference to the original item
//    }

//    throw new Exception("Not found");
//}

//int[] data = { 10, 20, 30 };
//ref int found = ref FindNumber(ref data, 20);
//found = 99;  // modifies the original array

//Console.WriteLine(data[1]);  // Output: 99

#endregion