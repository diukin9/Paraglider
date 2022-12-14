using Hangfire;
using Hangfire.MemoryStorage;
using Paraglider.API.BackgroundProcessing.ReccuringJobs.Gorko;
using Paraglider.API.Definitions.Base;
using Paraglider.Clients.Gorko;

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

        services.AddSingleton<GorkoClient>();
        services.AddScoped<GorkoReccuringJob>();
    }

    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseHangfireDashboard();
        BackgroundJob.Enqueue<GorkoReccuringJob>(_ => _.RunAsync());
        //RecurringJob.AddOrUpdate<GorkoReccuringJob>("gorko", service => service.RunAsync(), Cron.Daily);
    }
}
