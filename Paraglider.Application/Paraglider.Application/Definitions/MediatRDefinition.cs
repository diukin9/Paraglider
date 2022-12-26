using MediatR;
using Paraglider.Infrastructure.Common.AppDefinition;
using Paraglider.Infrastructure.Common.Attributes;
using System.Reflection;

namespace Paraglider.Application.Definitions;

[CallingOrder(7)]
public class MediatRDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}