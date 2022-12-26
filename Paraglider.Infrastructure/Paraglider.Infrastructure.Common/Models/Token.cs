using System.Text.Json.Serialization;

namespace Paraglider.Infrastructure.Common.Models;

public class Tokens
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = null!;

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = null!;

    [JsonPropertyName("expires_at")]
    public DateTime Expiration { get; set; }
}
