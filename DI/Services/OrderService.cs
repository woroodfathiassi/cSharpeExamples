using DI.DataAccess;
namespace DI.Services;

public class EmailSender
{
    public EmailSender()
    {

    }

    public void Send(string to, string body)
    {
        Console.WriteLine($"[EmailSender] Sending email to {to}: {body}");
    }
}

public class FileLogger
{
    public FileLogger()
    {

    }

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

        _emailSender.Send(order.CustomerEmail, "Your order received");
    }
}
