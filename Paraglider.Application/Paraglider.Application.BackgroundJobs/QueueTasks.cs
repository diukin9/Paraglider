using Hangfire;
using Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko;

namespace Paraglider.Application.BackgroundJobs;

public static class QueueTasks
{
    public static void Run()
    {
        RecurringJob.AddOrUpdate<ImportComponentsRecurringJob>(
            methodCall: service => service.RunAsync(),
            cronExpression: "* 0 * * *");

        RecurringJob.AddOrUpdate<UpdateComponentPopularityDataRecurringJob>(
            methodCall: service => service.RunAsync(),
            cronExpression: "0 * * * *");

        RecurringJob.AddOrUpdate<RemoveExpiredAndUnusedComponentsReccuringJob>(
            methodCall: service => service.RunAsync(),
            cronExpression: "0 * * * *");
    }
}
