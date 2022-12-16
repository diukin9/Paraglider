using MediatR;
using Paraglider.Infrastructure.Common.AppDefinition;
using System.Reflection;

namespace Paraglider.Application.Definitions;

public class MediatRDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}