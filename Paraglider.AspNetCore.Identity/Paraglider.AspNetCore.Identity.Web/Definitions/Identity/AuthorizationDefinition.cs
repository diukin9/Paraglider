using Paraglider.AspNetCore.Identity.Web.Definitions.Base;
using System.Security.Claims;

namespace Paraglider.AspNetCore.Identity.Web.Definitions.Identity
{
    /// <summary>
    /// Authorization Policy registration
    /// </summary>
    public class AuthorizationDefinition : AppDefinition
    {
        /// <summary>
        /// Configure services for current application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "Administrator");
                });

                options.AddPolicy("StandartUser", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "StandartUser");
                });
            });

            //TODO make extension
            services.AddAuthentication()
                .AddOAuth("Yandex.ru", "Yandex", config =>
                {
                    config.ClientId = configuration["Authentication:Yandex:ClientId"];
                    config.ClientSecret = configuration["Authentication:Yandex:ClientSecret"];
                    config.AuthorizationEndpoint = configuration["Authentication:Yandex:AuthorizationEndpoint"];
                    config.TokenEndpoint = configuration["Authentication:Yandex:TokenEndpoint"];
                    config.CallbackPath = "/api/yandex-authorization";
                });
        }

        /// <summary>
        /// Configure application for current application
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
        }
    }
}