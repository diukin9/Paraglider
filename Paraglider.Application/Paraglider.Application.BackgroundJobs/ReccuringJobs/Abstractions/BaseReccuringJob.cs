using Paraglider.Infrastructure.Common;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Paraglider.Application.BackgroundJobs.ReccuringJobs.Abstractions;

public abstract class BaseReccuringJob<T> : IReccuringJob
{
    private readonly string _logPath;
    protected readonly FileLogger _logger;

    public BaseReccuringJob()
    {
        var assembly = Assembly.GetExecutingAssembly()!;

        var info = new DirectoryInfo(GetThisFilePath());
        while (info!.Name != assembly.GetName().Name) info = info.Parent;

        var directory = Path.Combine(info.FullName, "Logs");
        var filename = $"{typeof(T).Name}Logs.txt";

        _logger = new FileLogger(directory, filename);
        _logPath = Path.Combine(directory, filename);
    }

    private static string GetThisFilePath([CallerFilePath] string path = default!)
    {
        return path;
    }

    protected void ClearLogs()
    {
        using var fs = File.Open(_logPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        lock (fs) { fs.SetLength(0); }
    }

    public abstract Task RunAsync();
}
