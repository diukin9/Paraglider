using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Paraglider.MobileApp.Services;

namespace Paraglider.MobileApp.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    public bool AuthorizeCommandIsNotRunning => !AuthorizeCommand.IsRunning;
    public bool AuthorizeByExternalProviderCommandIsNotRunning => !AuthorizeByExternalProviderCommand.IsRunning;
    public bool GoToForgotPasswordPageCommandIsNotRunning => !GoToForgotPasswordPageCommand.IsRunning;
    public bool GoToRegistrationPageCommandIsNotRunning => !GoToRegistrationPageCommand.IsRunning;

    [ObservableProperty]
    private string login;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private bool errorIsDisplayed;

    [ObservableProperty]
    private string errorMessage;

    [ObservableProperty]
    private bool loaderIsDisplayed;

    private readonly StorageService storageService;

    public LoginViewModel(StorageService storageService)
    {
        this.storageService = storageService;
    }

    [RelayCommand]
    private async Task GoToRegistrationPageAsync()
    {
        await NavigationService.GoToRegistrationPageAsync(true);
    }

    [RelayCommand]
    private async Task GoToForgotPasswordPageAsync()
    {
        await NavigationService.GoToForgotPasswordPageAsync(true);
    }

    [RelayCommand]
    private async Task Authorize()
    {
        ErrorIsDisplayed = false;
        LoaderIsDisplayed = true;

        var lastLoginDate = await storageService.GetLastLoginDateAsync();

        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
        {
            LoaderIsDisplayed = false;
            ErrorMessage = "Необходимо заполнить все поля";
            ErrorIsDisplayed = true;
            return;
        }

        var isSuccessful = await storageService.UpdateTokenAsync(login, password);

        LoaderIsDisplayed = false;

        if (!isSuccessful)
        {
            ErrorMessage = "Неверный логин или пароль";
            ErrorIsDisplayed = true;
        }
        else
        {
            if (lastLoginDate is null)
            {
                await NavigationService.GoToIntroPageAsync(true);
            }
            else
            {
                await NavigationService.GoToMainPageAsync(true);
            }
        }
    }

    [RelayCommand]
    private async Task AuthorizeByExternalProvider(string provider)
    {
        ErrorIsDisplayed = false;
        LoaderIsDisplayed = true;

        var lastLoginDate = await storageService.GetLastLoginDateAsync();
        var isSuccessful = await storageService.UpdateTokenAsync(provider);

        LoaderIsDisplayed = false;

        if (!isSuccessful)
        {
            ErrorMessage = "Не удалось авторизоваться";
            ErrorIsDisplayed = true;
        }
        else
        {
            if (lastLoginDate is null)
            {
                await NavigationService.GoToIntroPageAsync(true);
            }
            else
            {
                await NavigationService.GoToMainPageAsync(true);
            }
        }
    }
}
