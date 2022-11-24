using Microsoft.AspNetCore.HttpOverrides;
using Paraglider.API.Definitions.Base;

namespace Paraglider.API.Definitions.Common;

public class CommonDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddLocalization();
        services.AddHttpContextAccessor();
        services.AddResponseCaching();
        services.AddMemoryCache();

        services.AddControllers();
        services.AddApiVersioning();
    }

    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseHttpsRedirection();
        app.MapDefaultControllerRoute();
    }
}