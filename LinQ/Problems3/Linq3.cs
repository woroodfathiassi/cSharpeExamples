namespace LinQ.Problems3;

public class Linq3
{
    public static void Question1()
    {
        Console.WriteLine("\nQ1:");
        // Write a LINQ query to get the total quantity sold per category,
        // but only include categories where total quantity sold > 100.

        var categories = new List<Category>
        {
            new () { Id = 1, Name = "Electronics" },
            new () { Id = 2, Name = "Books" },
            new () { Id = 3, Name = "Clothing" },
            new () { Id = 4, Name = "Home & Kitchen" }
        };

        var products = new List<Product>
        {
            new () { Id = 101, CategoryId = 1 }, // Electronics
            new () { Id = 102, CategoryId = 1 },
            new () { Id = 201, CategoryId = 2 }, // Books
            new () { Id = 202, CategoryId = 2 },
            new () { Id = 301, CategoryId = 3 }, // Clothing
            new () { Id = 401, CategoryId = 4 }  // Home & Kitchen
        };

        var orders = new List<Order>
        {
            new () { ProductId = 101, Quantity = 120 },
            new () { ProductId = 102, Quantity = 100 },
            new () { ProductId = 201, Quantity = 50 },
            new () { ProductId = 201, Quantity = 65 },
            new () { ProductId = 301, Quantity = 300 },
            new () { ProductId = 401, Quantity = 10 }
        };

        var quantityMoreThan100 = orders.GroupBy(o => o.ProductId)
            .Select(g => new
            {
                ProductId = g.Key,
                Quantity = g.Sum(q => q.Quantity)
            })
            .Where(o => o.Quantity > 100);

        var result = from item in (from c in categories
                                   join p in products on c.Id equals p.CategoryId
                                   select new
                                   {
                                       pId = p.Id,
                                       cName = c.Name
                                   })
                     join o in quantityMoreThan100 on item.pId equals o.ProductId
                     select new
                     {
                         name = item.cName,
                         total = o.Quantity
                     };

        foreach (var item in result) Console.WriteLine($"{item.name} : {item.total}");
    }

    public static void Question2()
    {
        Console.WriteLine("\nQ2:");
        // You have a list of Customers, each with a list of Orders.
        // Each Order contains multiple OrderItems referencing a Product.
        // Return a list of customers and their total spend on "Electronics" products only,
        // sorted descending by total spend.

        var products = new List<Product2>
        {
            new () { Id = 1, Category = "Electronics", Price = 500m },
            new () { Id = 2, Category = "Electronics", Price = 1200m },
            new () { Id = 3, Category = "Books", Price = 20m },
            new () { Id = 4, Category = "Home & Kitchen", Price = 80m },
            new () { Id = 5, Category = "Electronics", Price = 150m }
        };

        var customers = new List<Customer>
        {
            new ()
            {
                Id = 1,
                Name = "Alice",
                Orders =
                [
                    new ()
                    {
                        Id = 101,
                        Items =
                        [
                            new () { ProductId = 1, Quantity = 1 }, // 500
                            new () { ProductId = 3, Quantity = 2 }  // (non-electronics)
                        ]
                    },
                    new ()
                    {
                        Id = 102,
                        Items =
                        [
                            new () { ProductId = 5, Quantity = 3 }  // 450
                        ]
                    }
                ]
            },
            new ()
            {
                Id = 2,
                Name = "Bob",
                Orders =
                [
                    new ()
                    {
                        Id = 201,
                        Items =
                        [
                            new () { ProductId = 2, Quantity = 1 }, // 1200
                            new () { ProductId = 4, Quantity = 2 }  // (non-electronics)
                        ]
                    }
                ]
            },
            new ()
            {
                Id = 3,
                Name = "Charlie",
                Orders =
                [
                    new ()
                    {
                        Id = 301,
                        Items =
                        [
                            new () { ProductId = 5, Quantity = 2 }, // 300
                            new () { ProductId = 3, Quantity = 1 }  // (non-electronics)
                        ]
                    },
                    new ()
                    {
                        Id = 302,
                        Items =
                        [
                            new () { ProductId = 1, Quantity = 2 }  // 1000
                        ]
                    }
                ]
            }
        };

        var www = customers.Select(c => new
        {
            name = c.Name,
            total = c.Orders.Select(o => new
            {
                elect = (from i in o.Items
                        join p in products on i.ProductId equals p.Id
                        where p.Category == "Electronics"
                        select new { price = p.Price * i.Quantity })
                        .Sum(s => s.price)

            }).Sum(i => i.elect)
        }).OrderByDescending(x => x.total);;

        foreach (var item in www)
            Console.WriteLine($"{item.name} : {item.total}");
    }
    public static void Question3()
    {
        Console.WriteLine("\nQ3:");
        // Group sales data by Region. For each region, generate:
        // · Total Sales
        // · Number of distinct customers
        // · Max single transaction
        // · Sales breakdown by high vs low (above 1000 vs ≤ 1000)

        var sales = new List<Sale>
        {
            new () { Region = "North", CustomerId = 101, Amount =  850m },
            new () { Region = "North", CustomerId = 102, Amount = 1200m },
            new () { Region = "North", CustomerId = 101, Amount =  300m },

            new () { Region = "South", CustomerId = 201, Amount = 2200m },
            new () { Region = "South", CustomerId = 202, Amount =  999m },
            new () { Region = "South", CustomerId = 203, Amount = 1500m },

            new () { Region = "East",  CustomerId = 301, Amount =  400m },
            new () { Region = "East",  CustomerId = 302, Amount =  700m },

            new () { Region = "West",  CustomerId = 401, Amount = 3000m },
            new () { Region = "West",  CustomerId = 402, Amount =  250m },
            new () { Region = "West",  CustomerId = 402, Amount =  850m }
        };

        var result =
            from s in sales
            group s by s.Region into g
            let high = g.Where(t => t.Amount > 1000m)
            let low = g.Where(t => t.Amount <= 1000m)
            select new
            {
                Region = g.Key,
                Total = g.Sum(t => t.Amount),
                DistinctCustomers = g.Select(t => t.CustomerId).Distinct().Count(),
                MaxTransaction = g.Max(t => t.Amount),
                High = new { Count = high.Count(), Sum = high.Sum(t => t.Amount) },
                Low = new { Count = low.Count(), Sum = low.Sum(t => t.Amount) }
            };

        foreach (var r in result)
        {
            Console.WriteLine($"Region: {r.Region}");
            Console.WriteLine(
                $"Total: {r.Total}, " +
                $"Cust: {r.DistinctCustomers}, " +
                $"Max: {r.MaxTransaction}, " +
                $"HighCnt: {r.High.Count} : {r.High.Sum} " +
                $"LowCnt: {r.Low.Count} : {r.Low.Sum}"
            );
            Console.WriteLine(); 
        }
    }

    public static void Question4()
    {
        Console.WriteLine("\nQ4:");
        // Given a list of Transactions with Date, Amount, and Category ,
        // group by month and category, and return total per group.

        var transactions = new List<Transaction>
        {
            new () { Date = new DateTime(2025, 1, 5), Category = "Food", Amount = 50 },
            new () { Date = new DateTime(2025, 1, 15), Category = "Food", Amount = 30 },
            new () { Date = new DateTime(2025, 1, 20), Category = "Transport", Amount = 20 },
            new () { Date = new DateTime(2025, 2, 5), Category = "Food", Amount = 40 },
            new () { Date = new DateTime(2025, 2, 10), Category = "Transport", Amount = 25 },
            new () { Date = new DateTime(2025, 2, 20), Category = "Shopping", Amount = 100 },
            new () { Date = new DateTime(2025, 3, 1), Category = "Food", Amount = 60 }
        };

        var result = transactions.GroupBy(t => new { Month = t.Date.ToString("yyyy-MM"), t.Category })
            .Select(g => new
            {
                g.Key.Month,
                g.Key.Category,
                TotalAmount = g.Sum(t => t.Amount)
            })
            .OrderBy(r => r.Month)
            .ThenBy(r => r.Category);

        foreach (var item in result)
        {
            Console.WriteLine($"{item.Month} | {item.Category} \nTotal: {item.TotalAmount:C}\n");
        }
    }
}

#region
public class Transaction
{
    public DateTime Date { get; set; }
    public string Category { get; set; }
    public decimal Amount { get; set; }
}

public class Sale
{
    public string Region { get; set; }
    public int CustomerId { get; set; }
    public decimal Amount { get; set; }
}

public class Order
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public int CategoryId { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Order2> Orders { get; set; }
}

public class Order2
{
    public int Id { get; set; }
    public List<OrderItem> Items { get; set; }
}

public class OrderItem
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}

public class Product2
{
    public int Id { get; set; }
    public string Category { get; set; }
    public decimal Price { get; set; }
}
#endregion