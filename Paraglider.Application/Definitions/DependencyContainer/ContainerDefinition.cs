using Paraglider.API.BackgroundProcessing.ReccuringJobs.Gorko;
using Paraglider.API.Definitions.Base;
using Paraglider.Clients.Gorko;

namespace Paraglider.API.Definitions.DependencyContainer;

public class ContainerDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");
    }
}