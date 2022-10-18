using Paraglider.AspNetCore.Identity.Domain;
using Paraglider.AspNetCore.Identity.Web.Definitions.Base;

namespace Paraglider.AspNetCore.Identity.Web.Definitions.Cors
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
            services.AddCors();
        }

        public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
        {
            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyMethod()
            );
        }
    }
}