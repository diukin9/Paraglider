namespace Paraglider.Data.MongoDB.BackgroundUploads.ReccuringJobs.Abstractions;

public interface IReccuringJob
{
    public Task RunAsync();
}
