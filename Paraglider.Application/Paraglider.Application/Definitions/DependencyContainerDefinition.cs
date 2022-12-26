using Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko;
using Paraglider.Infrastructure.Common.AppDefinition;
using Paraglider.Clients.Gorko;
using Paraglider.MailService.Models;
using Paraglider.MailService;
using Paraglider.Infrastructure.Common.Attributes;

namespace Paraglider.Application.Definitions;

[CallingOrder(11)]
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