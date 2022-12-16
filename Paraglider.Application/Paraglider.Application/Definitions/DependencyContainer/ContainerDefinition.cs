using Paraglider.Application.Definitions.Base;

namespace Paraglider.Application.Definitions.DependencyContainer;

public class ContainerDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration);
        services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");
    }
}