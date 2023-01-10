using Paraglider.Infrastructure.Common.AppDefinition;
using Paraglider.Infrastructure.Common.Attributes;

namespace Paraglider.Application.Definitions;

[CallingOrder(0)]
public class CookiePolicyDefinition : AppDefinition
{
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        // Add this before any other middleware that might write cookies
        app.UseCookiePolicy(new CookiePolicyOptions()
        {
            MinimumSameSitePolicy = SameSiteMode.Strict
        });
    }
}