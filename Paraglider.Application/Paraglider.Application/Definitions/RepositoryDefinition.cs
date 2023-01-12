using Paraglider.Data.EntityFrameworkCore;
using Paraglider.Infrastructure.Common.AppDefinition;
using Paraglider.Infrastructure.Common.Attributes;
using Paraglider.Infrastructure.Common.Extensions;
using System.Reflection;

namespace Paraglider.Application.Definitions;

[CallingOrder(8)]
public class RepositoryDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddRepositories(Assembly.GetAssembly(typeof(ApplicationDbContext))!);
    }
}
