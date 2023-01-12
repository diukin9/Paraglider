using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;

namespace Paraglider.Infrastructure.Common.Helpers;

public static class ClaimsPrincipalHelper
{
    public static ClaimsPrincipal CreateByUser<T, K>(T user) 
        where T : IdentityUser<K> 
        where K : IEquatable<K>
    {
        var identity = new ClaimsIdentity(
            CookieAuthenticationDefaults.AuthenticationScheme,
            ClaimTypes.Name,
            ClaimTypes.Role);

        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserName!));
        identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName!));
        identity.AddClaim(new Claim(ClaimTypes.Email, user.Email!));

        return new ClaimsPrincipal(identity);
    }
}
