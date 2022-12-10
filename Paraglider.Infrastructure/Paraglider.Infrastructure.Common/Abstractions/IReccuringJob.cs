namespace Paraglider.Infrastructure.Common.Abstractions;

public interface IReccuringJob
{
    public Task RunAsync();
}
