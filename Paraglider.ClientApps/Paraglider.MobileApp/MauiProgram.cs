using Microsoft.Extensions.Logging;
using Paraglider.MobileApp.Services;
using Paraglider.MobileApp.ViewModels;
using Paraglider.MobileApp.Views;

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
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.Services.AddSingleton(new HttpClient());

        builder.Services.AddSingleton<RestService>();
        builder.Services.AddTransient<TokenService>();

        builder.Services.AddTransient<WelcomePageViewModel>();
        builder.Services.AddTransient<LoginViewModel>();

        builder.Services.AddTransient<WelcomePage>();
        builder.Services.AddTransient<LoginPage>();

#if DEBUG
		    builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}