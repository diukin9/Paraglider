﻿using Paraglider.Web.Definitions.Base;

namespace Paraglider.Web.Definitions.Cors
{
    /// <summary>
    /// Cors configurations
    /// </summary>
    public class CorsDefinition : AppDefinition
    {
        /// <summary>
        /// Configure services for current application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            var origins = configuration.GetSection("Cors")?.GetSection("Origins")?.Value?.Split(',');
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    if (origins is not { Length: > 0 })
                    {
                        return;
                    }

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

        /// <summary>
        /// Configure application for current application
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
        {
            app.UseCors();
        }
    }
}