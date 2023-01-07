using Paraglider.MobileApp.Pages;
using Paraglider.MobileApp.Services;

namespace Paraglider.MobileApp.ViewModels;

public class LoadingPageViewModel
{
    private readonly StorageService storageService;

    public LoadingPageViewModel(StorageService storageService)
    {
        this.storageService = storageService;
        GoToNextPageAsync();
    }

    private async void GoToNextPageAsync()
    {
        var token = await storageService.GetTokenAsync();

        var page = token is null ? nameof(LoginPage) : nameof(MainPage);
        await Shell.Current.GoToAsync($"//{page}");
    }

}
