using Paraglider.API.Definitions.Base;
using Paraglider.Clients.Gorko;
using Paraglider.Data.MongoDB.BackgroundUploads.ReccuringJobs.Gorko;

namespace Paraglider.API.Definitions.DependencyContainer;

public class ContainerDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");

        services.AddSingleton<GorkoClient>();
        services.AddScoped<GorkoReccuringJob>();
    }
}