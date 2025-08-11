public class Order
{
    public string CustomerEmail { get; set; } = "";
    public int Quantity { get; set; }
    public string Item { get; set; } = "";
}


public class EmailSender
{
    public void Send(string to, string body)
    {
        Console.WriteLine($"[EmailSender] Sending email to {to}: {body}");

    }
}

public class FileLogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[FileLogger] {DateTime.Now:O} - {message}");

    }
}


public class OrderService
{
    private readonly EmailSender _emailSender = new (); 
    private readonly FileLogger _logger = new ();       

    public void PlaceOrder(Order order)
    {
        _logger.Log("Placing order...");

        _logger.Log($"Ordering {order.Quantity} x {order.Item}");
        _emailSender.Send(order.CustomerEmail, "Your order has been received. Thanks!");
        _logger.Log("Order placed successfully.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        var order = new Order
        {
            CustomerEmail = "customer@example.com",
            Quantity = 2,
            Item = "Keyboard"
        };

        var orderService = new OrderService();
        orderService.PlaceOrder(order);

        Console.WriteLine("Done (Before DI). Press any key to exit...");
        Console.ReadKey();
    }
}