using Hangfire;
using Hangfire.MemoryStorage;
using Paraglider.Application.BackgroundJobs;
using Paraglider.Infrastructure.Common.AppDefinition;
using Paraglider.Infrastructure.Common.Attributes;

namespace Paraglider.Application.Definitions;

[CallingOrder(5)]
public class HangfireDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        if (Convert.ToBoolean(configuration["Hangfire:UseHangfire"]))
        {
            var options = new MemoryStorageOptions()
            {
                FetchNextJobTimeout = TimeSpan.FromHours(12)
            };

            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMemoryStorage(options));

            services.AddHangfireServer();

            services.RegisterServicesForHangfireTasks();
        }
    }

    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        if (Convert.ToBoolean(app.Configuration["Hangfire:UseHangfire"]))
        {
            app.UseHangfireDashboard();
            QueueTasks.Run();
        }
    }
}
