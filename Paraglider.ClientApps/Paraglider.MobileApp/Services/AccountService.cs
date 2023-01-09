using Paraglider.MobileApp.Models;
using System.Text;
using System.Text.Json;
using static Paraglider.MobileApp.Constants;

namespace Paraglider.MobileApp.Services;

public class AccountService
{
    private readonly RestService restService;

    public AccountService(RestService restService)
    {
        this.restService = restService;
    }

    public async Task<bool> TryResetPasswordAsync(string email)
    {
        try
        {
            var url = $"{REST_URL}/account/reset-password";
            var body = JsonSerializer.Serialize(new { email });
            var requestContent = new StringContent(body, Encoding.UTF8, "application/json");
            var response = await restService.PostAsync<ResponseTemplate<string>>(url, requestContent, false);
            return response.IsOk;
        }
        catch
        {
            return false;
        }
    }
}
