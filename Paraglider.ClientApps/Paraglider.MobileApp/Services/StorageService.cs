using static Paraglider.MobileApp.Constants;

namespace Paraglider.MobileApp.Services;

public class StorageService
{
    private readonly AuthService authService;

    public StorageService(AuthService authService)
    {
        this.authService = authService;
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
            var token = await authService.UpdateTokenAsync(accessToken, refreshToken);
            if (token is not null) await SetAccessTokenAsync(token);
            return token?.AccessToken;
        }

        ClearRefreshTokenData();

        return null;
    }

    public async Task<bool> UpdateTokenAsync(string login, string password)
    {
        var token = await authService.GetTokenAsync(login, password);
        if (token is null) return false;

        await SetRefreshTokenAsync(token);
        await SetAccessTokenAsync(token);
        await SetLastLoginDateAsync(DateTime.UtcNow);

        return true;
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
}