using Paraglider.Application.BackgroundJobs.ReccuringJobs.Gorko;
using Paraglider.Application.Definitions.Base;
using Paraglider.Clients.Gorko;

namespace Paraglider.Application.Definitions.DependencyContainer;

public class ContainerDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");

        services.AddSingleton<GorkoClient>();
        services.AddScoped<ImportComponentsFromGorkoReccuringJob>();
    }
}