using Mapster;
using MapsterMapper;
using Paraglider.API.Definitions.Base;
using System.Reflection;

namespace Paraglider.API.Definitions.Mapping;

public class MapsterDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
        var mapperConfig = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapperConfig);
    }
}