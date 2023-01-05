using CommunityToolkit.Mvvm.Input;
using Paraglider.MobileApp.Platforms;
using Paraglider.MobileApp.Services;
using static Paraglider.MobileApp.Constants;

namespace Paraglider.MobileApp.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    public string Login { get; set; }
    public string Password { get; set; }

    private readonly TokenService tokenService;

    public LoginViewModel(TokenService tokenService)
    {
        this.tokenService = tokenService;
    }

    [RelayCommand]
    private async Task Authorize()
    {
        if (IsBusy) return;

        IsBusy = true;

        if (!await tokenService.AddOrUpdateTokensInSecureStorage(Login, Password))
        {
            await Shell.Current.DisplayAlert("Ошибка!", "Неверный логин или пароль", "Ок");
        }
        else
        {
            await Shell.Current.DisplayAlert("Оповещение", "Успешный вход", "Ок");
        }

        IsBusy = false;
    }

    [RelayCommand]
    private async Task AuthorizeByExternalProvider(string provider)
    {
        var authToken = string.Empty;
        var callbackUrl = new Uri($"{WebAuthenticationCallbackActivity.CALLBACK_SCHEME}://");
        var authUrl = new Uri($"{REST_URL}/token/{provider}?callbackUrl={callbackUrl}");

        var response = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);

        //if (response.Properties.TryGetValue("name", out var name) && !string.IsNullOrEmpty(name))
        //    authToken += $"Name: {name}{Environment.NewLine}";
        //if (response.Properties.TryGetValue("email", out var email) && !string.IsNullOrEmpty(email))
        //    authToken += $"Email: {email}{Environment.NewLine}";
        //authToken += response?.AccessToken ?? response?.IdToken;
    }

    [RelayCommand]
    private async Task CheckAuth()
    {
        if (IsBusy) return;

        IsBusy = true;

        var token = await tokenService.GetTokenFromSecureStorageAsync();

        await Shell.Current.DisplayAlert(
            "Проверка авторизации",
            token is null ? "Вы не авторизованы" : $"Ваш токен: {token}",
            "Ок");

        IsBusy = false;
    }
}
