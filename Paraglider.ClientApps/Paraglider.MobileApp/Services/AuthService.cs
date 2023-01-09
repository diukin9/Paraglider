using Paraglider.MobileApp.Platforms;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using static Paraglider.MobileApp.Constants;

namespace Paraglider.MobileApp.Services;

public class AuthService
{
    private readonly HttpClient httpClient;

    public AuthService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<Token> GetTokenAsync(string login, string password)
    {
        var url = new Uri($"{API_URL}/auth/mobile/token");
        var body = JsonSerializer.Serialize(new { login, password });
        var requestContent = new StringContent(body, Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PostAsync(url, requestContent);
            var content = await response.Content.ReadAsStreamAsync();
            var token = await JsonSerializer.DeserializeAsync<Token>(content);
            return token?.RefreshToken is null || token?.AccessToken is null ? null : token;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get token: {ex.Message}");
            return null;
        }
    }


    public async Task<Token> GetTokenAsync(string scheme)
    {
        try
        {
            var callbackUrl = new Uri($"{WebAuthenticationCallbackActivity.CALLBACK_SCHEME}://");
            var authUrl = new Uri($"{API_URL}/auth/mobile/{scheme}");
            var response = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);

            response.Properties.TryGetValue("access_token", out var accessToken);
            response.Properties.TryGetValue("access_token_expires_at", out var accessTokenExpTime);
            response.Properties.TryGetValue("refresh_Token", out var refreshToken);
            response.Properties.TryGetValue("refresh_token_expires_at", out var refreshTokenExpTime);

            return new Token
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenExpTime,
                AccessTokenExpiryTime = DateTime.Parse(accessTokenExpTime),
                RefreshTokenExpiryTime = DateTime.Parse(refreshTokenExpTime)
            };
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{scheme.ToUpper()} | Unable to get token: {ex.Message}");
            return null;
        }
    }

    public async Task<Token> UpdateTokenAsync(string expiredAccessToken, string refreshToken)
    {
        var url = new Uri($"{API_URL}/auth/mobile/token");
        var body = JsonSerializer.Serialize(new
        {
            expired_access_token = expiredAccessToken,
            refresh_token = refreshToken
        });
        var requestContent = new StringContent(body, Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PutAsync(url, requestContent);
            var content = await response.Content.ReadAsStreamAsync();
            var token = await JsonSerializer.DeserializeAsync<Token>(content);
            return token?.RefreshToken is null || token?.AccessToken is null ? null : token;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to update token: {ex.Message}");
            return null;
        }
    }
}
