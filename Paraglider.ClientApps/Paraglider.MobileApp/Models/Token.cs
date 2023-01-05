using System.Text.Json.Serialization;

public class Token
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = null!;

    [JsonPropertyName("access_token_expires_at")]
    public DateTime AccessTokenExpiryTime { get; set; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = null!;

    [JsonPropertyName("refresh_token_expires_at")]
    public DateTime RefreshTokenExpiryTime { get; set; }
}
