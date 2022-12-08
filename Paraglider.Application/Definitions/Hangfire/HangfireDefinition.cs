using Hangfire;
using Hangfire.MemoryStorage;
using Paraglider.API.Definitions.Base;
using Paraglider.Data.MongoDB.BackgroundUploads.ReccuringJobs.Gorko;

namespace Paraglider.API.Definitions.Hangfire;

public class HangfireDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseMemoryStorage());

        services.AddHangfireServer();
    }

    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseHangfireDashboard();

        RecurringJob.AddOrUpdate<GorkoReccuringJob>("gorko", service => service.RunAsync(), Cron.Minutely);
    }
}
