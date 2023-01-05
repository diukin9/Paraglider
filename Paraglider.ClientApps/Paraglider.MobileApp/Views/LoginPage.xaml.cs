using Paraglider.MobileApp.ViewModels;

namespace Paraglider.MobileApp.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext= viewModel;
        Title = "Страница авторизации";
    }
}