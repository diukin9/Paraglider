using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using Paraglider.Infrastructure.Common.AppDefinition;
using Paraglider.Infrastructure.Common.Attributes;
using Paraglider.Infrastructure.Common.Models;

namespace Paraglider.Application.Definitions;

[CallingOrder(2)]
public class AuthDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = configuration["Authentication:Bearer:Issuer"],

            ValidateAudience = true,
            ValidAudience = configuration["Authentication:Bearer:Audience"],

            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Authentication:Bearer:Key"]!)),

            ValidateLifetime = true
        };

        services.AddSingleton(tokenValidationParameters);

        services
            .AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddJwtBearer(config =>
            {
                config.TokenValidationParameters = tokenValidationParameters;
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
            })
            .AddYandex(config =>
            {
                config.ClientId = configuration["Authentication:Yandex:ClientId"]!;
                config.ClientSecret = configuration["Authentication:Yandex:ClientSecret"]!;
                config.CorrelationCookie = new CookieBuilder
                {
                    SameSite = SameSiteMode.Lax
                };
            })
            .AddVkontakte(config =>
            {
                config.ClientId = configuration["Authentication:Vkontakte:ClientId"]!;
                config.ClientSecret = configuration["Authentication:Vkontakte:ClientSecret"]!;
                config.CorrelationCookie = new CookieBuilder
                {
                    SameSite = SameSiteMode.Lax
                };
            });
            
            services.AddAuthorization();

        var bearerSettings = new BearerSettings();
        configuration.Bind("Authentication:Bearer", bearerSettings);
        services.AddSingleton(bearerSettings);
    }

    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
    }
}