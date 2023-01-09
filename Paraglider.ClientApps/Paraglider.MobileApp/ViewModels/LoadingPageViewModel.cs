using Paraglider.MobileApp.Pages;
using Paraglider.MobileApp.Services;

namespace Paraglider.MobileApp.ViewModels;

public class LoadingPageViewModel
{
    private readonly StorageService storageService;
    private readonly NavigationService navigationService;

    public LoadingPageViewModel(StorageService storageService, NavigationService navigationService)
    {
        this.storageService = storageService;
        this.navigationService = navigationService;

        GoToStartPageAsync();
    }

    private async void GoToStartPageAsync()
    {
        var token = await storageService.GetTokenAsync();

        if (token is null)
        {
            await navigationService.GoToAsync<LoginPage>();
        }
        else
        {
            await navigationService.GoToAsync<MainPage>();
        }
    }
}
