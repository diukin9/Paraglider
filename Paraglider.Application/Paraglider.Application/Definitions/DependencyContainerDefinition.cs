using Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko;
using Paraglider.Infrastructure.Common.AppDefinition;
using Paraglider.Clients.Gorko;
using Paraglider.MailService.Models;
using Paraglider.MailService;

namespace Paraglider.Application.Definitions;

public class DependencyContainerDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");

        services.AddSingleton<GorkoClient>();
        services.AddScoped<ImportComponentsFromGorkoRecurringJob>();
        services.AddScoped<UpdateComponentPopularityDataRecurringJob>();

        var mailSettings = new MailSettings();
        configuration.Bind(nameof(MailSettings), mailSettings);
        services.AddSingleton(mailSettings);
        services.AddScoped<IMailService, MailService.MailService>();
    }
}