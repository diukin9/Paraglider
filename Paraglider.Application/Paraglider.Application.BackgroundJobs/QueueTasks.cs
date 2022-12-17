using Hangfire;
using Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko;
using Paraglider.Domain.Common.Enums;

namespace Paraglider.Application.BackgroundJobs;

public static class QueueTasks
{
    public static void Run()
    {
        RecurringJob.AddOrUpdate<ImportComponentsFromGorkoRecurringJob>(
            methodCall: service => service.RunAsync(),
            cronExpression: "* 0 * * *",
            queue: Source.Gorko.ToString().ToLower());

        RecurringJob.AddOrUpdate<UpdateComponentPopularityDataRecurringJob>(
            methodCall: service => service.RunAsync(),
            cronExpression: "0 * * * *",
            queue: Source.Gorko.ToString().ToLower());
    }
}
