using Paraglider.MobileApp.ViewModels;

namespace Paraglider.MobileApp.Pages;

public partial class LoginPage : ContentPage
{
    private bool signInViaYandexButton_IsAnimating;
    private bool signInViaVkontakteButton_IsAnimating;

    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext= viewModel;
    }

    private async void SignInViaYandexButton_Clicked(object sender, EventArgs e)
    {
        if (!signInViaYandexButton_IsAnimating)
        {
            signInViaYandexButton_IsAnimating = true;
            await SignInViaYandexButton.ScaleTo(1.07);
            await SignInViaYandexButton.ScaleTo(1);
            signInViaYandexButton_IsAnimating = false;
        }
    }

    private async void SignInViaVkontakteButton_Clicked(object sender, EventArgs e)
    {
        if (!signInViaVkontakteButton_IsAnimating)
        {
            signInViaVkontakteButton_IsAnimating = true;
            await SignInViaVkontakteButton.ScaleTo(1.07);
            await SignInViaVkontakteButton.ScaleTo(1);
            signInViaVkontakteButton_IsAnimating = false;
        }
    }

    private void HideKeyboard(object sender, TappedEventArgs e)
    {
        EmailEntry.IsEnabled = false;
        EmailEntry.IsEnabled = true;

        PasswordEntry.IsEnabled = false;
        PasswordEntry.IsEnabled = true;
    }

    private void HideKeyboard(object sender, EventArgs e)
    {
        PasswordEntry.IsEnabled = false;
        PasswordEntry.IsEnabled = true;
    }

    private void EmailEntry_Focused(object sender, FocusEventArgs e)
    {
        EmailEntry.Text = string.Empty;
    }

    private void PasswordEntry_Focused(object sender, FocusEventArgs e)
    {
        PasswordEntry.Text = string.Empty;
    }
}