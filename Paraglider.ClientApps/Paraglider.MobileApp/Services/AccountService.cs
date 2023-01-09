using Paraglider.MobileApp.Models;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using static Paraglider.MobileApp.Constants;

namespace Paraglider.MobileApp.Services;

public class AccountService
{
    private readonly HttpClient httpClient;

    public AccountService(HttpClient httpClient)
    {
        this.httpClient = httpClient;
    }

    public async Task<bool> TrySendEmailToResetPasswordAsync(string email)
    {
        var url = $"{API_URL}/account/reset-password";
        var body = JsonSerializer.Serialize(new { email });
        var requestContent = new StringContent(body, Encoding.UTF8, "application/json");

        try
        {
            var response = await httpClient.PostAsync(url, requestContent);
            var content = await response.Content.ReadAsStreamAsync();
            var result = await JsonSerializer.DeserializeAsync<ResponseTemplate>(content);
            return result.IsOk;
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Failed to send password recovery email: {ex.Message}");
            return false;
        }
    }
}