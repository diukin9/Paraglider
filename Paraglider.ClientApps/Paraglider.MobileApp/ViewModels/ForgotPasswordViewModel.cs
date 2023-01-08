using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Paraglider.MobileApp.Helpers;
using Paraglider.MobileApp.Services;

namespace Paraglider.MobileApp.ViewModels;

public partial class ForgotPasswordViewModel : BaseViewModel
{
    public bool ResetPasswordCommandIsNotRunning => !ResetPasswordCommand.IsRunning;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private bool errorIsDisplayed;

    [ObservableProperty]
    private string errorMessage;

    [ObservableProperty]
    private string imagePath = GetDefaultImagePath();

    [ObservableProperty]
    private bool loaderIsDisplayed;

    [ObservableProperty]
    private int msgLabelRow = 3;

    private readonly AccountService accountService;
    private readonly NavigationService navigationService;

    public ForgotPasswordViewModel(NavigationService navigationService, AccountService accountService)
    {
        this.navigationService = navigationService;
        this.accountService = accountService;
    }

    [RelayCommand]
    private async Task ResetPasswordAsync(Grid grid)
    {        
        ErrorIsDisplayed = false;
        LoaderIsDisplayed = true;

        if (string.IsNullOrEmpty(email))
        {
            LoaderIsDisplayed = false;
            ErrorMessage = "Необходимо заполнить все поля";
            ErrorIsDisplayed = true;
            return;
        }

        if (!StringHelper.IsEmail(email))
        {
            LoaderIsDisplayed = false;
            ErrorMessage = "Неверный формат почтового ящика";
            ErrorIsDisplayed = true;
            return;
        }

        var msgLabel = (Label)grid.FindByName("messageLabel");
        var cancelLabel = (Label)grid.FindByName("cancelLink");
        var btn = (Button)grid.FindByName("submit_btn");
        var input = (VerticalStackLayout)grid.FindByName("input");

        var emailIsSent = await accountService.TryResetPasswordAsync(email);

        LoaderIsDisplayed = false;

        if (emailIsSent)
        {
            input.IsVisible = false;
            cancelLabel.IsVisible = false;

            btn.Text = "Назад";
            btn.Command = GoToPreviousPageCommand;

            msgLabel.HorizontalTextAlignment = TextAlignment.Center;
            MsgLabelRow = 4;
            msgLabel.FormattedText = GenerateFormattedStringWhenEmailIsSent();

            ImagePath = GetImagePathWhenEmailIsSent();
        }
        else
        {
            msgLabel.FormattedText = GenerateFormattedStringWhenEmailIsNotSent();
            ImagePath = GetImagePathWhenEmailIsNotSent();
        }
    }

    [RelayCommand]
    private async Task GoToPreviousPageAsync()
    {
        await navigationService.GoBackAsync(true);
    }

    private static string GetDefaultImagePath()
    {
        return "forgot_password_default.svg";
    }

    private static string GetImagePathWhenEmailIsSent()
    {
        return "forgot_password_email_is_sent.svg";
    }

    private static string GetImagePathWhenEmailIsNotSent()
    {
        return "forgot_password_email_is_not_sent.svg";
    }

    private FormattedString GenerateFormattedStringWhenEmailIsNotSent()
    {
        var formattedString = new FormattedString();

        formattedString.Spans.Add(new Span()
        {
            Text = "Письмо не было отправлено, проверьте " +
                   "правильность введённой электронной почты",
            FontSize = 16,
            TextColor = new Color(144, 144, 144),
            FontFamily = "geometria_medium"
        });

        return formattedString;
    }

    private FormattedString GenerateFormattedStringWhenEmailIsSent()
    {
        var formattedString = new FormattedString();

        formattedString.Spans.Add(new Span()
        {
            Text = "На вашу электронную почту",
            FontSize = 16,
            TextColor = new Color(144, 144, 144),
            FontFamily = "geometria_medium"
        });

        formattedString.Spans.Add(new Span()
        {
            Text = $" {email} ",
            FontSize = 16,
            TextColor = new Color(58, 58, 58),
            FontFamily = "geometria_medium"
        });

        formattedString.Spans.Add(new Span()
        {
            Text = "было отправлено письмо с ссылкой для восстановления пароля",
            FontSize = 16,
            TextColor = new Color(144, 144, 144),
            FontFamily = "geometria_medium"
        });

        return formattedString;
    }
}
