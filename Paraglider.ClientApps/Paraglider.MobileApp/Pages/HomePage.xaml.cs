using Paraglider.MobileApp.Services;

namespace Paraglider.MobileApp.Pages;

public partial class HomePage : ContentPage
{
	private readonly StorageService storageService;

	public HomePage(StorageService storageService)
	{
		this.storageService = storageService;

		InitializeComponent();
	}


    //TODO temp
    private async void Button_Clicked(object sender, EventArgs e)
    {
		storageService.ClearTokenData();
		await NavigationService.GoToLoginPageAsync();
    }
}