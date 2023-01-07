namespace Paraglider.MobileApp.Services;

public class NavigationService
{
    readonly IServiceProvider _services;
    protected INavigation Navigation => Application.Current.MainPage.Navigation;

    public NavigationService(IServiceProvider services)
    { 
        _services = services;
    }

    public async Task GoToAsync<T>(bool withAnimation = false) where T : Page
    {
        var page = ResolvePage<T>();

        if (page is not null)
        {
            await Navigation.PushAsync(page, withAnimation);
            return;
        }

        throw new InvalidOperationException($"Unable to resolve type {typeof(T).FullName}");
    }

    public async Task GoBackAsync(bool withAnimation = false)
    {
        await Navigation.PopAsync(withAnimation);
    }

    private T ResolvePage<T>() where T : Page
    {
        return _services.GetService<T>();
    }
}