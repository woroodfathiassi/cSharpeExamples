using Microsoft.Extensions.DependencyInjection;


public class Order
{
    public string CustomerEmail { get; set; } = "";
    public int Quantity { get; set; }
    public string Item { get; set; } = "";
}


public interface IEmailSender
{
    void Send(string to, string body);
}

public interface ILogger
{
    void Log(string message);
}



public class SmtpEmailSender : IEmailSender
{
    public void Send(string to, string body)
    {
        Console.WriteLine($"[SmtpEmailSender] Sending email to {to}: {body}");

    }
}

public class FileLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[FileLogger] {DateTime.Now:O} - {message}");

    }
}


public class OrderService
{
    private readonly IEmailSender _emailSender;
    private readonly ILogger _logger;

    public OrderService(IEmailSender emailSender, ILogger logger)
    {
        _emailSender = emailSender;
        _logger = logger;
    }

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

        var services = new ServiceCollection();


        services.AddTransient<IEmailSender, SmtpEmailSender>();
        services.AddSingleton<ILogger, FileLogger>();           
        services.AddTransient<OrderService>();                 


        using var provider = services.BuildServiceProvider();
        var orderService = provider.GetRequiredService<OrderService>();

        var order = new Order
        {
            CustomerEmail = "customer@example.com",
            Quantity = 2,
            Item = "Keyboard"
        };

        orderService.PlaceOrder(order);

        Console.WriteLine("Done (After DI). Press any key to exit...");
        Console.ReadKey();
    }
}