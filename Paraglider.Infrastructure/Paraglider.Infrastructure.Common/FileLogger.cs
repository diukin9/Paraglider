using Microsoft.Extensions.Logging;

namespace Paraglider.Infrastructure.Common;

public class FileLogger : ILogger, IDisposable
{
    private readonly string filePath;
    private static readonly object _lock = new();

    public FileLogger(string directory, string name)
    {
        var path = Path.Combine(directory, name);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
            File.Create(path);
        }
        filePath = path;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return this;
    }

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        lock (_lock)
        {
            File.AppendAllText(
                filePath, 
                formatter(state, exception) + Environment.NewLine);
        }
    }

    public void Dispose() { }
}
