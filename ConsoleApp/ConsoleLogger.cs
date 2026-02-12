namespace ConsoleApp;

public class ConsoleLogger : ILogger
{
    public void LogInfo(string message)
    {
        Console.WriteLine($"[INFO] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
    }

    public void LogError(string message, Exception exception)
    {
        Console.WriteLine($"[ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
            
        if (exception != null)
        {
            Console.WriteLine($"Exception: {exception.Message}");
            Console.WriteLine($"Stack Trace: {exception.StackTrace}");
        }
    }
}