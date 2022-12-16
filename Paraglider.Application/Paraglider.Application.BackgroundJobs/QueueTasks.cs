using Hangfire;
using Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko;
using Paraglider.Domain.Common.Enums;

namespace Paraglider.Application.BackgroundJobs;

public static class QueueTasks
{
    public static void Run()
    {
        RecurringJob.AddOrUpdate<ImportComponentsFromGorkoReccuringJob>(
            Source.Gorko.ToString(),
            service => service.RunAsync(),
            "* 0 * * *");
    }
}
