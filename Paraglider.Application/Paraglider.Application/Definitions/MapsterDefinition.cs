using Mapster;
using MapsterMapper;
using Paraglider.Application.BackgroundJobs.ReccuringJobs.Abstractions;
using Paraglider.Infrastructure.Common.AppDefinition;
using Paraglider.Data.EntityFrameworkCore;
using System.Reflection;
using Paraglider.Infrastructure.Common.Attributes;

namespace Paraglider.Application.Definitions;

[CallingOrder(6)]
public class MapsterDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;

        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly()!);
        typeAdapterConfig.Scan(Assembly.GetAssembly(typeof(ApplicationDbContext))!);
        typeAdapterConfig.Scan(Assembly.GetAssembly(typeof(ComponentDataAccess))!);
        typeAdapterConfig.Scan(Assembly.GetAssembly(typeof(IReccuringJob))!);

        var mapperConfig = new Mapper(typeAdapterConfig);
        services.AddSingleton<IMapper>(mapperConfig);
    }
}