using Microsoft.AspNetCore.HttpOverrides;
using Paraglider.Infrastructure.Common.AppDefinition;

namespace Paraglider.Application.Definitions;

public class CommonDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient();
        services.AddLocalization();
        services.AddHttpContextAccessor();
        services.AddResponseCaching();
        services.AddMemoryCache();

        services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
        });
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