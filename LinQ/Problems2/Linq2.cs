using LinQ.Problems1;

namespace LinQ.Problems2;

public class Linq2
{
    public static void Question1()
    {
        Console.WriteLine("\n\nQ1: ");
        //You have a list of Customer objects, each with a list of Orders.
        //Write a LINQ query to get a flattened list of all orders,
        //including the customer name and order total.

        var customers = new List<Customer>();

        customers.AddRange(
        [
            new Customer
            {
                Name = "Ahmad",
                Orders = [new Order { OrderId = 1, Total = 10 }]
            },
            new Customer
            {
                Name = "Jod",
                Orders = [new Order { OrderId = 2, Total = 20 }]
            },
            new Customer
            {
                Name = "Ali",
                Orders = [new Order { OrderId = 3, Total = 30 }]
            }
        ]);

        var orders = customers.SelectMany(c => c.Orders, (c, o) => new
        {
            customerName = c.Name,
            orderTotal = o.Total
        });

        foreach (var order in orders) Console.WriteLine($"{order.customerName} - {order.orderTotal}");
    }

    public static void Question2()
    {
        Console.WriteLine("\n\nQ2: ");
        //Group Sales data by region, and for each region, calculate the total sales,
        //average sales, and number of high-value sales (sales over 1000).

        var sales = new List<Sale>
        {
            new () { Region = "North", Amount = 1200m },
            new () { Region = "North", Amount = 800m },
            new () { Region = "East",  Amount = 950m },
            new () { Region = "West",  Amount = 1500m },
            new () { Region = "East", Amount = 1100m }
        };

        var result = sales.GroupBy(s => s.Region).Select(r => new
        {
            regin = r.Key,
            total = r.Sum(r=>r.Amount),
            average = r.Average(r=>r.Amount),
            highSale = r.Max(r=>r.Amount),
        });
        Console.WriteLine($"{"Region",-10} {"Total",10} {"Average",10} {"High Sale",10}");
        Console.WriteLine(new string('-', 45));

        foreach (var r in result)
        {
            Console.WriteLine($"{r.regin,-10} {r.total,10:C} {r.average,10:C} {r.highSale,10:C}");
        }
    }

    public static void Question3()
    {
        Console.WriteLine("\n\nQ3: ");
        //Join Orders with Products to get a report of total quantity sold per product category.

        var orders = new List<OrderQ3>
        {
            new () { ProductId = 1, Quantity = 5 },
            new () { ProductId = 2, Quantity = 3 },
            new () { ProductId = 3, Quantity = 7 },
            new () { ProductId = 1, Quantity = 2 },
            new () { ProductId = 4, Quantity = 6 }
        };
        var products = new List<Product>
        {
            new () { Id = 1, Category = "Books" },
            new () { Id = 2, Category = "Books" },
            new () { Id = 3, Category = "Clothing" },
            new () { Id = 4, Category = "Clothing" },
            new () { Id = 5, Category = "Sports" }
        };

        var result =
            (from p in products
             join o in orders on p.Id equals o.ProductId into orderGroup
             from o in orderGroup.DefaultIfEmpty() // LEFT JOIN
             group o by p.Category into g
             select new
             {
                 Category = g.Key,
                 TotalSold = g.Sum(x => x?.Quantity ?? 0) 
             });

        foreach (var r in result) Console.WriteLine($"{r.Category} - Total Sold: {r.TotalSold}");
    }
}

#region Classes
public class Customer
{
    public string Name { get; set; }
    public List<Order> Orders { get; set; }
}

public class Order
{
    public int OrderId { get; set; }
    public decimal Total { get; set; }
}

public class Sale
{
    public string Region { get; set; }
    public decimal Amount { get; set; }
}

public class OrderQ3

{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Category { get; set; }
}
#endregion
