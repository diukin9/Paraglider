using Paraglider.MobileApp.ViewModels;

namespace Paraglider.MobileApp.Pages;

public partial class LoadingPage : ContentPage
{
	public LoadingPage(LoadingPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}