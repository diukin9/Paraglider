using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Abstractions;
using System.Reflection;

namespace Paraglider.API.BackgroundProcessing.ReccuringJobs;

public abstract class ReccuringJob<T> : IReccuringJob
{
    protected readonly FileLogger _logger;

    public ReccuringJob()
    {
        var assembly = Assembly.GetAssembly(typeof(ReccuringJob<>))!;

        var info = new DirectoryInfo(assembly.Location);
        while (info!.Name != assembly.GetName().Name) info = info.Parent;

        var directory = Path.Combine(info.FullName, "Logs");
        var filename = $"{typeof(T).Name}Logs.txt";

        _logger = new FileLogger(directory, filename);
    }

    public abstract Task RunAsync();
}
