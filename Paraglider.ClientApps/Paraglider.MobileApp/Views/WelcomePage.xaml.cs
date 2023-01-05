using Paraglider.MobileApp.ViewModels;

namespace Paraglider.MobileApp.Views;

public partial class WelcomePage : ContentPage
{
	public WelcomePage(WelcomePageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}