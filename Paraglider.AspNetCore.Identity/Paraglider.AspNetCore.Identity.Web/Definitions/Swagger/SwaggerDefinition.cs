using Microsoft.OpenApi.Models;
using Paraglider.AspNetCore.Identity.Infrastructure.AppData;
using Paraglider.AspNetCore.Identity.Web.Application.Attributes;
using Paraglider.AspNetCore.Identity.Web.Definitions.Base;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Paraglider.AspNetCore.Identity.Web.Definitions.Swagger
{
    /// <summary>
    /// Swagger definition for application
    /// </summary>
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

                options.TagActionsBy(api =>
                {
                    string tag;
                    if (api.ActionDescriptor is { } descriptor)
                    {
                        var attribute = descriptor.EndpointMetadata.OfType<FeatureGroupNameAttribute>().FirstOrDefault();
                        tag = attribute?.GroupName ?? descriptor.RouteValues["controller"] ?? "Untitled";
                    }
                    else
                    {
                        tag = api.RelativePath!;
                    }

                    var tags = new List<string>();
                    if (!string.IsNullOrEmpty(tag))
                    {
                        tags.Add(tag);
                    }
                    return tags;
                });
            });
        }

        public override void ConfigureApplication(WebApplication app, IWebHostEnvironment environment)
        {
            if (!app.Environment.IsDevelopment())
            {
                return;
            }

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
}