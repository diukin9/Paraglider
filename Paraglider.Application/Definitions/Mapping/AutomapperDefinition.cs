using Paraglider.Web.Definitions.Base;

namespace Paraglider.Web.Definitions.Mapping;

public class AutomapperDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Program));
    }

    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        var mapper = app.Services.GetRequiredService<AutoMapper.IConfigurationProvider>();
        if (env.IsDevelopment())
        {
            // validate Mapper Configuration
            mapper.AssertConfigurationIsValid();
        }
        else
        {
            mapper.CompileMappings();
        }
    }
}