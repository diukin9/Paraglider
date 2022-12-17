using Paraglider.Infrastructure.Common.AppDefinition;

namespace Paraglider.Application.Definitions;

public class AuthDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication()
            .AddYandex(config =>
            {
                config.ClientId = configuration["Authentication:Yandex:ClientId"]!;
                config.ClientSecret = configuration["Authentication:Yandex:ClientSecret"]!;
            })
            .AddVkontakte(config =>
            {
                config.ClientId = configuration["Authentication:Vkontakte:ClientId"]!;
                config.ClientSecret = configuration["Authentication:Vkontakte:ClientSecret"]!;
            });
        services.AddAuthorization();
    }

    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
    }
}