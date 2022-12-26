using Paraglider.Infrastructure.Common.AppDefinition;
using Paraglider.Infrastructure.Common.Attributes;

namespace Paraglider.Application.Definitions;

[CallingOrder(3)]
public class CorsDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var origins = configuration.GetSection("Cors")?.GetSection("Origins")?.Value?.Split(',');
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                if (origins is not { Length: > 0 }) return;

                if (origins.Contains("*"))
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.SetIsOriginAllowed(host => true);
                    builder.AllowCredentials();
                }
                else
                {
                    foreach (var origin in origins)
                    {
                        builder.WithOrigins(origin);
                    }
                }
            });
        });
    }

    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseCors();
    }
}