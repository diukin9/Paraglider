using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;

namespace Paraglider.Infrastructure.Common.Extensions;

public static class HttpContextExtensions
{
    public static string? GetNameIdentifier(this HttpContext httpContext)
    {
        var nameIdentifier = httpContext.GetNameIdentifierFromToken();
        return nameIdentifier ?? httpContext.GetNameIdentifierFromCookie();
    }

    public static string? GetNameIdentifierFromCookie(this HttpContext httpContext)
    {
        return httpContext.User.Identity?.Name;
    }

    public static string? GetNameIdentifierFromToken(this HttpContext httpContext)
    {
        var token = httpContext.Request.Headers.ContainsKey(HeaderNames.Authorization)
            ? $"{httpContext.Request.Headers[HeaderNames.Authorization]}".Replace("Bearer ", "")
            : null;

        if (token is not null)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(token);

            return jwtSecurityToken.Claims
                .Where(claim => claim.Type == JwtRegisteredClaimNames.Sub)
                .Single().Value;
        }

        return null;
    }
}
