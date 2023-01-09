using Paraglider.MobileApp.Services;

namespace Paraglider.MobileApp.ViewModels;

public class LoadingPageViewModel
{
    private readonly StorageService storageService;

    public LoadingPageViewModel(StorageService storageService)
    {
        this.storageService = storageService;

        GoToStartPageAsync();
    }

    private async void GoToStartPageAsync()
    {
        var token = await storageService.GetTokenAsync();

        if (token is null)
        {
            await NavigationService.GoToLoginPageAsync();
        }
        else
        {
            await NavigationService.GoToMainPageAsync();
        }
    }
}
