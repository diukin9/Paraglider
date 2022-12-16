using MediatR;
using Paraglider.Application.Definitions.Base;
using System.Reflection;

namespace Paraglider.Application.Definitions.Mediator;

public class MediatorDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}