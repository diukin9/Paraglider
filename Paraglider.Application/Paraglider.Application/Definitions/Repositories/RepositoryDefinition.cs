using Paraglider.Application.Definitions.Base;
using Paraglider.Data.EntityFrameworkCore;
using Paraglider.Data.MongoDB;
using Paraglider.Infrastructure.Common.Extensions;
using System.Reflection;

namespace Paraglider.Application.Definitions.Repositories;

public class RepositoryDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMongoDataAccess(Assembly.GetAssembly(typeof(ComponentDataAccess))!);

        services.AddRepositories(Assembly.GetAssembly(typeof(ApplicationDbContext))!);
    }
}
