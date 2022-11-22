using Paraglider.API.Definitions.Base;
using Paraglider.Data;
using Paraglider.Data.MongoDB;
using Paraglider.Infrastructure.Common.Extensions;
using System.Reflection;

namespace Paraglider.API.Definitions.Repositories;

public class RepositoryDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        //регистрируем MongoDataAccess для NoSql-репозиториев
        services.AddMongoDataAccess(Assembly.GetAssembly(typeof(WeddingComponentDataAccess))!);

        services.AddRepositories(Assembly.GetAssembly(typeof(ApplicationDbContext))!);
        services.AddRepositories(Assembly.GetAssembly(typeof(WeddingComponentDataAccess))!);
    }
}
