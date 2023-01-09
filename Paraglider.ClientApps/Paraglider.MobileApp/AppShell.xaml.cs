using CommunityToolkit.Maui.Behaviors;
using CommunityToolkit.Maui.Core;

namespace Paraglider.MobileApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        Behaviors.Add(new StatusBarBehavior
        {
            StatusBarColor = new Color(58, 58, 58),
            StatusBarStyle = StatusBarStyle.LightContent
        });
        base.OnAppearing();
    }
}