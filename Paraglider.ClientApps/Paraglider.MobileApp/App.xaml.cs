using Paraglider.MobileApp.Services;

namespace Paraglider.MobileApp;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        MainPage = new AppShell();
    }
}