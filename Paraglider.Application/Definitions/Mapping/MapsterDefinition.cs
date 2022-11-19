using Mapster;
using MapsterMapper;
using Paraglider.API.Definitions.Base;
using Paraglider.Data;
using Paraglider.Data.MongoDB;
using System.Reflection;

namespace Paraglider.API.Definitions.Mapping;

public class MapsterDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;

        typeAdapterConfig.Scan(Assembly.GetAssembly(typeof(ApplicationDbContext))!);
        typeAdapterConfig.Scan(Assembly.GetAssembly(typeof(WeddingComponentDataAccess))!);

        var mapperConfig = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapperConfig);
    }
}