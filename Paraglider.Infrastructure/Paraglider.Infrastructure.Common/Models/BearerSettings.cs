namespace Paraglider.Infrastructure.Common.Models;

public class BearerSettings
{
    public string Key { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Audience { get; set; } = null!;
    public long AccessTokenLifetimeInTicks { get; set; }
    public long RefreshTokenLifetimeInTicks { get; set; }
}
