using Paraglider.Web.Definitions.Base;

namespace Paraglider.Web.Definitions.Identity
{
    /// <summary>
    /// Authorization Policy registration
    /// </summary>
    public class AuthDefinition : AppDefinition
    {
        /// <summary>
        /// Configure services for current application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication()
                .AddYandex(config =>
                {
                    config.ClientId = configuration["Authentication:Yandex:ClientId"];
                    config.ClientSecret = configuration["Authentication:Yandex:ClientSecret"];
                })
                .AddVkontakte(config =>
                {
                    config.ClientId = configuration["Authentication:Vkontakte:ClientId"];
                    config.ClientSecret = configuration["Authentication:Vkontakte:ClientSecret"];
                });
            services.AddAuthorization();
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