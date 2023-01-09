namespace Paraglider.MobileApp.Services;

public static class NavigationService
{
    public static async Task GoToMainPageAsync(bool withAnimation = false)
    {
        await Shell.Current.GoToAsync("//MainPage", withAnimation);
    }

    public static async Task GoToRegistrationPageAsync(bool withAnimation = false)
    {
        await Shell.Current.GoToAsync("//RegistrationPage", withAnimation);
    }

    public static async Task GoToLoginPageAsync(bool withAnimation = false)
    {
        await Shell.Current.GoToAsync("//LoginPage", withAnimation);
    }

    public static async Task GoToIntroPageAsync(bool withAnimation = false)
    {
        await Shell.Current.GoToAsync("//IntroPage", withAnimation);
    }

    public static async Task GoToForgotPasswordPageAsync(bool withAnimation = false)
    {
        await Shell.Current.GoToAsync("//ForgotPasswordPage", withAnimation);
    }

    public static async Task GoToLoadingPageAsync(bool withAnimation = false)
    {
        await Shell.Current.GoToAsync("//LoadingPage", withAnimation);
    }
}