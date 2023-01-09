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

        //pages
        builder.Services.AddSingleton<LoadingPage>();
        builder.Services.AddTransient<CatalogPage>();
        builder.Services.AddTransient<HomePage>();
        builder.Services.AddTransient<IntroPage>();
        builder.Services.AddTransient<ForgotPasswordPage>();
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<RegistrationPage>();
        builder.Services.AddTransient<PlanningPage>();

        //services
        builder.Services.AddTransient<StorageService>();
        builder.Services.AddTransient<AccountService>();

        //viewmodels
        builder.Services.AddTransient<LoginViewModel>();
        builder.Services.AddSingleton<IntroPageViewModel>();
        builder.Services.AddSingleton<LoadingPageViewModel>();
        builder.Services.AddTransient<ForgotPasswordViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        return builder.Build();
    }
}