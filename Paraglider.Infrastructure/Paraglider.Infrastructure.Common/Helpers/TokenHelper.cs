using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Paraglider.Infrastructure.Common.Helpers;

public static class TokenHelper
{
    public static string GenerateAccessToken(
        string key,
        string issuer,
        string audience,
        List<Claim> claims,
        DateTime expires)
    {
        var ssKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        var credentials = new SigningCredentials(ssKey, SecurityAlgorithms.HmacSha256);

        var jwtToken = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expires,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }

    public static string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public static ClaimsPrincipal? GetPrincipalFromExpiredToken(
        string accessToken, 
        TokenValidationParameters validationParameters)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var principal = tokenHandler.ValidateToken(
            token: accessToken, 
            validationParameters: validationParameters, 
            validatedToken: out SecurityToken securityToken);

        var alg = SecurityAlgorithms.HmacSha256;
        var strComparasion = StringComparison.InvariantCultureIgnoreCase;

        return securityToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg.Equals(alg, strComparasion)
                ? null : principal;
    }
}
