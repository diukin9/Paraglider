using Paraglider.Infrastructure.Common.AppDefinition;
using Paraglider.Infrastructure.Common.Attributes;

namespace Paraglider.Application.Definitions;

[CallingOrder(0)]
public class CookiePolicyDefinition : AppDefinition
{
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseCookiePolicy(new CookiePolicyOptions()
        {
            OnAppendCookie = options =>
            {
                options.CookieOptions.SameSite = SameSiteMode.None;
                options.CookieOptions.Secure = true;
            }
        });
    }
}