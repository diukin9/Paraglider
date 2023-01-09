using Paraglider.MobileApp.Platforms;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using static Paraglider.MobileApp.Constants;

namespace Paraglider.MobileApp.Services;

public class StorageService
{
    private readonly HttpClient httpClient;

    public StorageService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<DateTime?> GetLastLoginDateAsync()
    {
        var value = await SecureStorage.GetAsync(LAST_LOGIN_DATE);
        return value is null ? null : DateTime.Parse(value);
    }

    public async Task<string> GetTokenAsync()
    {
        var accessToken = await GetAccessTokenAsync();
        var accessTokenExpTime = await GetAccessTokenExpiryTimeAsync();

        if (accessToken != null && accessTokenExpTime.HasValue && DateTime.UtcNow < accessTokenExpTime)
        {
            return accessToken;
        }

        ClearAccessTokenData();

        var refreshToken = await GetRefreshTokenAsync();
        var refreshTokenExpTime = await GetRefreshTokenExpiryTimeAsync();

        if (refreshToken != null && refreshTokenExpTime.HasValue && DateTime.UtcNow < refreshTokenExpTime)
        {
            var token = await UpdateExpiredTokenAsync(accessToken, refreshToken);
            if (token is not null) await SetAccessTokenAsync(token);
            return token?.AccessToken;
        }

        ClearRefreshTokenData();

        return null;
    }

    public async Task<bool> UpdateTokenAsync(string scheme)
    {
        var token = await GetTokenAsync(scheme);
        if (token is null) return false;
        await UpdateTokenAsync(token); 
        return true;
    }

    public async Task<bool> UpdateTokenAsync(string login, string password)
    {
        var token = await GetTokenAsync(login, password);
        if (token is null) return false;
        await UpdateTokenAsync(token);
        return true;
    }

    public void ClearTokenData()
    {
        ClearRefreshTokenData();
        ClearAccessTokenData();
    }

    private async Task UpdateTokenAsync(Token token)
    {
        await SetRefreshTokenAsync(token);
        await SetAccessTokenAsync(token);
        await SetLastLoginDateAsync(DateTime.UtcNow);
    }

    private async Task<string> GetAccessTokenAsync()
    {
        return await SecureStorage.GetAsync(ACCESS_TOKEN_KEY);
    }

    private async Task<DateTime?> GetAccessTokenExpiryTimeAsync()
    {
        var value = await SecureStorage.GetAsync(ACCESS_TOKEN_EXPIRY_TIME_KEY);
        var result = DateTime.TryParse(value, out var accessTokenExpiryTime);
        return result ? accessTokenExpiryTime : null;
    }

    private async Task<string> GetRefreshTokenAsync()
    {
        return await SecureStorage.GetAsync(REFRESH_TOKEN_KEY);
    }

    private async Task<DateTime?> GetRefreshTokenExpiryTimeAsync()
    {
        var value = await SecureStorage.GetAsync(REFRESH_TOKEN_EXPIRY_TIME_KEY);
        var result = DateTime.TryParse(value, out var refreshTokenExpiryTime);
        return result ? refreshTokenExpiryTime : null;
    }

    private void ClearAccessTokenData()
    {
        SecureStorage.Remove(ACCESS_TOKEN_KEY);
        SecureStorage.Remove(ACCESS_TOKEN_EXPIRY_TIME_KEY);
    }

    private void ClearRefreshTokenData()
    {
        SecureStorage.Remove(REFRESH_TOKEN_KEY);
        SecureStorage.Remove(REFRESH_TOKEN_EXPIRY_TIME_KEY);
    }

    private async Task SetAccessTokenAsync(Token token)
    {
        await SecureStorage.SetAsync(ACCESS_TOKEN_KEY, token.AccessToken);
        await SecureStorage.SetAsync(ACCESS_TOKEN_EXPIRY_TIME_KEY, $"{token.AccessTokenExpiryTime}");
    }

    private async Task SetRefreshTokenAsync(Token token)
    {
        await SecureStorage.SetAsync(REFRESH_TOKEN_KEY, token.RefreshToken);
        await SecureStorage.SetAsync(REFRESH_TOKEN_EXPIRY_TIME_KEY, $"{token.RefreshTokenExpiryTime}");
    }

    private async Task SetLastLoginDateAsync(DateTime dateTime)
    {
        await SecureStorage.SetAsync(LAST_LOGIN_DATE, $"{dateTime}");
    }

    private async Task<Token> GetTokenAsync(string login, string password)
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

    private async Task<Token> GetTokenAsync(string scheme)
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

            if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(accessTokenExpTime)
                || string.IsNullOrEmpty(refreshToken) || string.IsNullOrEmpty(refreshTokenExpTime))
            {
                throw new InvalidOperationException("Failed to get full data about the token");
            }

            return new Token
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenExpTime,
                AccessTokenExpiryTime = DateTime.Parse(accessTokenExpTime),
                RefreshTokenExpiryTime = DateTime.Parse(refreshTokenExpTime)
            };
        }
        catch (TaskCanceledException ex)
        {
            Debug.WriteLine($"Task was canceled: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"{scheme.ToUpper()} | Unable to get token: {ex.Message}");
            return null;
        }
    }

    private async Task<Token> UpdateExpiredTokenAsync(string expiredAccessToken, string refreshToken)
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