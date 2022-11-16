using MediatR;
using Paraglider.Web.Definitions.Base;
using System.Reflection;

namespace Paraglider.Web.Definitions.Mediator;

public class MediatorDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}