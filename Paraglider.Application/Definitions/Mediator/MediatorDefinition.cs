using MediatR;
using Paraglider.API.Definitions.Base;
using System.Reflection;

namespace Paraglider.API.Definitions.Mediator;

public class MediatorDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}