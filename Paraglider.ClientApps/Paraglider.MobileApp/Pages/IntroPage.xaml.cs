using Paraglider.MobileApp.ViewModels;

namespace Paraglider.MobileApp.Pages;

public partial class IntroPage : ContentPage
{
    public IntroPage(IntroPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}