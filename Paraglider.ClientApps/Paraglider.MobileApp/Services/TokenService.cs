using System.Diagnostics;
using System.Text;
using System.Text.Json;
using static Paraglider.MobileApp.Constants;

namespace Paraglider.MobileApp.Services;

public class TokenService
{
    private readonly HttpClient httpClient;

    public TokenService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<string> GetTokenFromSecureStorageAsync()
    {
        //получаем access_token из хранилища
        var accessToken = await SecureStorage.GetAsync(ACCESS_TOKEN_KEY);
        
        var isSuccessful = DateTime.TryParse(
            await SecureStorage.GetAsync(ACCESS_TOKEN_EXPIRY_TIME_KEY),
            out var accessTokenExpiryTime);

        //если срок действия access_token не истек - возвращаем его
        if (isSuccessful && DateTime.UtcNow < accessTokenExpiryTime)
        {
            return accessToken;
        }

        //удаляем данные о истекшем access_token из хранилища
        SecureStorage.Remove(ACCESS_TOKEN_KEY);
        SecureStorage.Remove(ACCESS_TOKEN_EXPIRY_TIME_KEY);

        //получаем refresh_token из хранилища
        var refreshToken = await SecureStorage.GetAsync(REFRESH_TOKEN_KEY);

        isSuccessful = DateTime.TryParse(
            await SecureStorage.GetAsync(REFRESH_TOKEN_EXPIRY_TIME_KEY),
            out var refreshTokenExpiryTime);

        //если срок действия refresh_token не истек - получаем новый access_token
        if (isSuccessful && DateTime.UtcNow < refreshTokenExpiryTime)
        {
            //получаем обновленный токен
            var token = await UpdateTokenAsync(accessToken, refreshToken);

            //сохраним его в безопасном хранилище
            if (token is not null)
            {
                await SecureStorage.SetAsync(ACCESS_TOKEN_KEY, token.AccessToken);
                await SecureStorage.SetAsync(
                    key: ACCESS_TOKEN_EXPIRY_TIME_KEY,
                    value: token.AccessTokenExpiryTime.ToString());
            }

            return token?.AccessToken;
        }

        //удаляем данные о истекшем refresh_token из хранилища
        SecureStorage.Remove(REFRESH_TOKEN_KEY);
        SecureStorage.Remove(REFRESH_TOKEN_EXPIRY_TIME_KEY);

        return null;
    }

    public async Task<bool> AddOrUpdateTokensInSecureStorage(string login, string password)
    {
        var token = await GetTokenAsync(login, password);
        if (token is null) return false;

        await SecureStorage.SetAsync(REFRESH_TOKEN_KEY, token.RefreshToken);
        await SecureStorage.SetAsync(
            key: REFRESH_TOKEN_EXPIRY_TIME_KEY,
            value: token.RefreshTokenExpiryTime.ToString());

        await SecureStorage.SetAsync(ACCESS_TOKEN_KEY, token.AccessToken);
        await SecureStorage.SetAsync(
            key: ACCESS_TOKEN_EXPIRY_TIME_KEY,
            value: token.AccessTokenExpiryTime.ToString());

        return true;
    }

    private async Task<Token> GetTokenAsync(string login, string password)
    {
        var url = new Uri($"{REST_URL}/token");
        var body = JsonSerializer.Serialize(new { login, password });
        var requestContent = new StringContent(body, Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PostAsync(url, requestContent);
            var content = await response.Content.ReadAsStreamAsync();

            var token = await JsonSerializer.DeserializeAsync<Token>(content);

            return token?.RefreshToken is null || token?.AccessToken is null ? null : token;
        }
        catch(Exception ex)
        {
            Debug.WriteLine($"Unable to get token: {ex.Message}");
            return null;
        }     
    }

    private async Task<Token> UpdateTokenAsync(string expiredAccessToken, string refreshToken)
    {
        var url = new Uri($"{REST_URL}/token");
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
        catch(Exception ex)
        {
            Debug.WriteLine($"Unable to update token: {ex.Message}");
            return null;
        }
    }
}