using Paraglider.MobileApp.ViewModels;

namespace Paraglider.MobileApp.Pages;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage(RegisterViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    private void HideKeyboard(object sender, TappedEventArgs e)
    {
        HideKeyboard();
    }

    private void HideKeyboard(object sender, EventArgs e)
    {
        HideKeyboard();
    }

    private void HideKeyboard()
    {
        EmailEntry.IsEnabled = false;
        EmailEntry.IsEnabled = true;

        FirstNameEntry.IsEnabled = false;
        FirstNameEntry.IsEnabled = true;

        PasswordEntry.IsEnabled = false;
        PasswordEntry.IsEnabled = true;

        SurnameEntry.IsEnabled = false;
        SurnameEntry.IsEnabled = true;

        PasswordConfirmationEntry.IsEnabled = false;
        PasswordConfirmationEntry.IsEnabled = true;

        CityEntry.IsEnabled = false;
        CityEntry.IsEnabled = true;
    }

    private void EmailEntry_Focused(object sender, FocusEventArgs e)
    {
        EmailEntry.Text = string.Empty;
    }

    private void PasswordConfirmationEntry_Focused(object sender, FocusEventArgs e)
    {
        PasswordConfirmationEntry.Text = string.Empty;
    }

    private void SurnameEntry_Focused(object sender, FocusEventArgs e)
    {
        SurnameEntry.Text = string.Empty;
    }

    private void PasswordEntry_Focused(object sender, FocusEventArgs e)
    {
        PasswordEntry.Text = string.Empty;
    }

    private void FirstNameEntry_Focused(object sender, FocusEventArgs e)
    {
        FirstNameEntry.Text = string.Empty;
    }
}