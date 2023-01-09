using CommunityToolkit.Mvvm.ComponentModel;

namespace Paraglider.MobileApp.ViewModels;

public partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private string title;
}
