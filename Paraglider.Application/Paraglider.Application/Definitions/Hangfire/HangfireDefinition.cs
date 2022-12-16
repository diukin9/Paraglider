using Hangfire;
using Hangfire.MemoryStorage;
using Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko;
using Paraglider.Application.Definitions.Base;
using Paraglider.Clients.Gorko;
using Paraglider.Domain.Common.Enums;

namespace Paraglider.Application.Definitions.Hangfire;

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

            services.AddSingleton<GorkoClient>();
            services.AddScoped<ImportComponentsFromGorkoReccuringJob>();
        }
    }

    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        if (Convert.ToBoolean(app.Configuration["Hangfire:UseInterface"]))
        {
            app.UseHangfireDashboard();
        }

        RecurringJob.AddOrUpdate<ImportComponentsFromGorkoReccuringJob>(
            Source.Gorko.ToString(),
            service => service.RunAsync(),
            "* 0 * * *");
    }
}
