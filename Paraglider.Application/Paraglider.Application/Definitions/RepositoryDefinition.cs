using Paraglider.Infrastructure.Common.AppDefinition;
using Paraglider.Data.EntityFrameworkCore;
using Paraglider.Data.MongoDB;
using Paraglider.Infrastructure.Common.Extensions;
using System.Reflection;
using Paraglider.Infrastructure.Common.Attributes;

namespace Paraglider.Application.Definitions;

[CallingOrder(8)]
public class RepositoryDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDataAccess(Assembly.GetAssembly(typeof(ComponentDataAccess))!);
        services.AddRepositories(Assembly.GetAssembly(typeof(ApplicationDbContext))!);
    }
}
