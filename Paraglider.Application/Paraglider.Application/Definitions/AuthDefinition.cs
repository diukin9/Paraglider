using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Paraglider.Infrastructure.Common.AppDefinition;
using Paraglider.Infrastructure.Common.Attributes;
using Paraglider.Infrastructure.Common.Models;
using System.Text;

namespace Paraglider.Application.Definitions;

[CallingOrder(2)]
public class AuthDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration config)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = Convert.ToBoolean(config["Authentication:Bearer:ValidateIssuer"]),
            ValidIssuer = config["Authentication:Bearer:Issuer"],
            ValidateAudience = Convert.ToBoolean(config["Authentication:Bearer:ValidateAudience"]),
            ValidAudience = config["Authentication:Bearer:Audience"],
            ValidateIssuerSigningKey = Convert.ToBoolean(config["Authentication:Bearer:ValidateSigningKey"]),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(config["Authentication:Bearer:Key"]!)),
            ValidateLifetime = Convert.ToBoolean(config["Authentication:Bearer:ValidateLifetime"])
        };

        services
            .AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = "JWT_OR_COOKIE"; ;
                options.DefaultScheme = "JWT_OR_COOKIE";
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, config =>
            {
                config.TokenValidationParameters = tokenValidationParameters;
                config.RequireHttpsMetadata = false;
                config.SaveToken = true;
            })
            .AddPolicyScheme("JWT_OR_COOKIE", "JWT_OR_COOKIE", options =>
            {
                options.ForwardDefaultSelector = context =>
                {
                    var header = (string?)context.Request.Headers[HeaderNames.Authorization];
                    if (header?.StartsWith(JwtBearerDefaults.AuthenticationScheme) ?? false)
                    {
                        return JwtBearerDefaults.AuthenticationScheme;
                    }

                    return CookieAuthenticationDefaults.AuthenticationScheme;
                };
            })
            .AddYandex(options =>
            {
                options.ClientId = config["Authentication:Yandex:ClientId"]!;
                options.ClientSecret = config["Authentication:Yandex:ClientSecret"]!;
            })
            .AddVkontakte(options =>
            {
                options.ClientId = config["Authentication:Vkontakte:ClientId"]!;
                options.ClientSecret = config["Authentication:Vkontakte:ClientSecret"]!;
            });

        services.AddAuthorization(options => 
        {
            options.DefaultPolicy = new AuthorizationPolicyBuilder(
                CookieAuthenticationDefaults.AuthenticationScheme,
                JwtBearerDefaults.AuthenticationScheme).RequireAuthenticatedUser().Build();
        });

        services.AddSingleton(tokenValidationParameters);

        var bearerSettings = new BearerSettings();
        config.Bind("Authentication:Bearer", bearerSettings);
        services.AddSingleton(bearerSettings);
    }

    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();
    }
}