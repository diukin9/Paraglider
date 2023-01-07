using Microsoft.Extensions.Logging;
using Paraglider.MobileApp.Pages;
using Paraglider.MobileApp.Services;
using Paraglider.MobileApp.ViewModels;
using CommunityToolkit.Maui;

namespace Paraglider.MobileApp;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("geometria_bold.otf", "geometria_bold");
                fonts.AddFont("geometria_light.otf", "geometria_light");
                fonts.AddFont("geometria_medium.otf", "geometria_medium");
            })
            .UseMauiCommunityToolkit();

        builder.Services.AddSingleton(new HttpClient());

        builder.Services.AddSingleton<RestService>();
        builder.Services.AddTransient<StorageService>();

        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddTransient<LoadingPageViewModel>();

        builder.Services.AddSingleton<LoadingPage>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<ForgotPasswordPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<RegistrationPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}