using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using Paraglider.MobileApp.Pages;
using Paraglider.MobileApp.Platforms;
using Paraglider.MobileApp.Services;
using static Paraglider.MobileApp.Constants;

namespace Paraglider.MobileApp.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    public string Login { get; set; }
    public string Password { get; set; }

    private readonly StorageService storageService;

    public LoginViewModel(StorageService storageService)
    {
        this.storageService = storageService;
    }

    [RelayCommand]
    private async Task GoToRegistrationPageAsync()
    {
        if (IsBusy) return;

        IsBusy = true;

        await Shell.Current.GoToAsync($"//{nameof(RegistrationPage)}");

        IsBusy = false;
    }

    [RelayCommand]
    private async Task GoToForgotPasswordPageAsync()
    {
        if (IsBusy) return;

        IsBusy = true;

        await Shell.Current.GoToAsync($"//{nameof(ForgotPasswordPage)}");

        IsBusy = false;
    }

    [RelayCommand]
    private async Task Authorize()
    {
        if (IsBusy) return;

        if (string.IsNullOrEmpty(Login) || string.IsNullOrEmpty(Password))
        {
            await Snackbar.Make(
                message: "Необходимо заполнить все поля",
                actionButtonText: "X",
                duration: TimeSpan.FromSeconds(3),
                visualOptions: new SnackbarOptions
                {
                    BackgroundColor = new Color(245, 245, 245),
                    TextColor = new Color(58, 58, 58),
                    CornerRadius = new CornerRadius(15)
                }).Show();

            return;
        }

        IsBusy = true;

        if (!await storageService.AddOrUpdateToken(Login, Password))
        {
            await Snackbar.Make(
                message: "Неверный логин или пароль",
                actionButtonText: "X",
                duration: TimeSpan.FromSeconds(3),
                visualOptions: new SnackbarOptions
                {
                    BackgroundColor = new Color(245, 245, 245),
                    TextColor = new Color(58, 58, 58),
                    CornerRadius = new CornerRadius(15)
                }).Show();
        }
        else
        {
            var page = await storageService.GetLastLoginDateAsync() is null
                ? nameof(WelcomePage) : nameof(MainPage);

            await Shell.Current.GoToAsync($"//{page}");
        }

        IsBusy = false;
    }

    [RelayCommand]
    private async Task AuthorizeByExternalProvider(string provider)
    {
        var authToken = string.Empty;
        var callbackUrl = new Uri($"{WebAuthenticationCallbackActivity.CALLBACK_SCHEME}://");
        var authUrl = new Uri($"{REST_URL}/auth/common/{provider}?callback={callbackUrl}&authType=1");

        var response = await WebAuthenticator.AuthenticateAsync(authUrl, callbackUrl);

        //if (response.Properties.TryGetValue("name", out var name) && !string.IsNullOrEmpty(name))
        //    authToken += $"Name: {name}{Environment.NewLine}";
        //if (response.Properties.TryGetValue("email", out var email) && !string.IsNullOrEmpty(email))
        //    authToken += $"Email: {email}{Environment.NewLine}";
        //authToken += response?.AccessToken ?? response?.IdToken;
    }
}
