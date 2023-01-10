using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Paraglider.MobileApp.Helpers;
using Paraglider.MobileApp.Infrastructure.Exceptions;
using Paraglider.MobileApp.Models;
using Paraglider.MobileApp.Services;
using System.Collections.ObjectModel;

namespace Paraglider.MobileApp.ViewModels;

public partial class RegisterViewModel : BaseViewModel
{
    [ObservableProperty]
    private string imagePath = GetDefaultImagePath();

    [ObservableProperty]
    private bool switched;

    [ObservableProperty]
    private bool errorIsDisplayed;

    [ObservableProperty]
    private string errorMessage;

    [ObservableProperty]
    private bool loaderIsDisplayed;

    [ObservableProperty]
    private bool notSwitched = true;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private string passwordConfirmation;

    [ObservableProperty]
    private string firstName;

    [ObservableProperty]
    private string surname;

    [ObservableProperty]
    private City city;

    [ObservableProperty]
    private ObservableCollection<City> cities;

    private readonly AccountService accountService;
    private readonly CityService cityService;

    public RegisterViewModel(AccountService accountService, CityService cityService)
    {
        this.accountService = accountService;
        this.cityService = cityService;
    }

    [RelayCommand]
    private async Task GetCitiesAsync()
    {
        Cities = new ObservableCollection<City>(await cityService.GetCitiesAsync());
    }

    [RelayCommand]
    private async Task GoToLoginPageAsync()
    {
        await NavigationService.GoToLoginPageAsync(true);
    }

    [RelayCommand]
    private async Task OnButtonClicked(Grid grid)
    {
        ErrorIsDisplayed = false;
        LoaderIsDisplayed = true;

        if (notSwitched)
        {
            if (StringHelper.IsNullOrEmpty(email, password, passwordConfirmation))
            {
                ShowWarning("Необходимо заполнить все поля");
                return;
            }

            if (!StringHelper.IsEmail(email))
            {
                ShowWarning("Неверный формат почтового ящика");
                return;
            }

            if (password.Length < 8)
            {
                ShowWarning("Минимальная длина пароля - 8 символов");
                return;
            }

            if (password != passwordConfirmation)
            {
                ShowWarning("Пароли не совпадают");
                return;
            }

            LoaderIsDisplayed = false;

            Switch();
        }
        else
        {
            if (StringHelper.IsNullOrEmpty(firstName, surname) || city is null)
            {
                ShowWarning("Необходимо заполнить все поля");
                return;
            }

            var model = new RegisterModel
            {
                FirstName = firstName,
                Surname = surname,
                Email = email,
                Password = password,
                CityId = city.Id
            };

            bool isSuccessful;

            try 
            {
                isSuccessful = await accountService.RegisterAsync(model);
            }
            catch(DuplicateException)
            {
                ShowWarning("Пользователь с таким email уже существует");
                return;
            }

            if (!isSuccessful)
            {
                ShowWarning("Не удалось зарегистрироваться");
            }
            else
            {
                var goBack = (HorizontalStackLayout)grid.FindByName("GoBack");
                var firstNameInput = (VerticalStackLayout)grid.FindByName("FirstNameInput");
                var surnameInput = (VerticalStackLayout)grid.FindByName("SurnameInput");
                var cityInput = (VerticalStackLayout)grid.FindByName("CityInput");
                var btn = (Button)grid.FindByName("Btn");
                var label = (Label)grid.FindByName("MessageWhenEmailIsSent");

                LoaderIsDisplayed = false;

                goBack.IsVisible = false;
                firstNameInput.IsVisible = false;
                surnameInput.IsVisible = false;
                cityInput.IsVisible = false;

                label.FormattedText = GenerateFormattedStringWhenEmailIsSent();
                label.IsVisible = true;

                btn.Text = "Ок";
                btn.Command = GoToLoginPageCommand;

                ImagePath = GetImagePathWhenEmailIsSent();
            }
        }
    }

    [RelayCommand]
    private void ToggleSwitch()
    {
        Switch();
    }

    private void Switch()
    {
        Switched = notSwitched;
        NotSwitched = !switched;
    }

    private void ShowWarning(string message)
    {
        LoaderIsDisplayed = false;
        ErrorMessage = message;
        ErrorIsDisplayed = true;
    }

    private static string GetDefaultImagePath()
    {
        return "registration_default.svg";
    }

    private static string GetImagePathWhenEmailIsSent()
    {
        return "registration_email_is_sent.svg";
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
            Text = $"\n{email}\n",
            FontSize = 16,
            TextColor = new Color(58, 58, 58),
            FontFamily = "geometria_medium",

        });

        formattedString.Spans.Add(new Span()
        {
            Text = "было отправлено письмо с ссылкой " +
                   "для подтверждения почтового ящика",
            FontSize = 16,
            TextColor = new Color(144, 144, 144),
            FontFamily = "geometria_medium"
        });

        return formattedString;
    }
}
