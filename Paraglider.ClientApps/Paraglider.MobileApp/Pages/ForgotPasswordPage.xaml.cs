using Paraglider.MobileApp.ViewModels;

namespace Paraglider.MobileApp.Pages;

public partial class ForgotPasswordPage : ContentPage
{
	public ForgotPasswordPage(ForgotPasswordViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}

    private void HideKeyboard(object sender, EventArgs e)
    {
        EmailEntry.IsEnabled = false;
        EmailEntry.IsEnabled = true;
    }

    private void HideKeyboard(object sender, TappedEventArgs e)
    {
        EmailEntry.IsEnabled = false;
        EmailEntry.IsEnabled = true;
    }

    private void EmailEntry_Focused(object sender, FocusEventArgs e)
    {
        EmailEntry.Text = string.Empty;
    }
}