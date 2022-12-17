namespace Paraglider.Application.BackgroundJobs.ReccuringJobs.Abstractions;

public interface IReccuringJob
{
    public Task RunAsync();
}
