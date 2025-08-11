namespace DependencyInjection.Services;

public class LoggerService
{
    public void Log(string message)
    {
        Console.WriteLine($"[Logger]: {message}");
    }
}
