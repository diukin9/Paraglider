using Microsoft.OpenApi.Models;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.AppDefinition;
using Paraglider.Infrastructure.Common.Attributes;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;
using Unchase.Swashbuckle.AspNetCore.Extensions.Extensions;

namespace Paraglider.Application.Definitions;

[CallingOrder(9)]
public class SwaggerDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = AppData.ServiceName,
            });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            options.AddEnumsWithValuesFixFilters();
        });
    }

    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment environment)
    {
        app.UseSwagger();
        app.UseSwaggerUI(settings =>
        {
            settings.DocumentTitle = $"{AppData.ServiceName}";
            settings.DefaultModelExpandDepth(0);
            settings.DefaultModelRendering(ModelRendering.Model);
            settings.DocExpansion(DocExpansion.None);
            settings.DisplayRequestDuration();
        });
    }
}