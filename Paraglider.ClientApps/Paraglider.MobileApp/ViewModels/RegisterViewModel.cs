using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Paraglider.MobileApp.Helpers;
using Paraglider.MobileApp.Models;
using Paraglider.MobileApp.Services;
using System.Collections.ObjectModel;

namespace Paraglider.MobileApp.ViewModels;

public partial class RegisterViewModel : BaseViewModel
{
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
    private string city;

    [ObservableProperty]
    private ObservableCollection<City> cities = new();

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
    private async Task OnButtonClicked()
    {
        ErrorIsDisplayed = false;
        LoaderIsDisplayed = true;

        if (notSwitched)
        {
            if (StringHelper.IsNullOrEmpty(email, password, passwordConfirmation))
            {
                LoaderIsDisplayed = true;
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

            if (password.Length < 8)
            {
                LoaderIsDisplayed = false;
                ErrorMessage = "Пароль должен содержать минимум 8 символов";
                ErrorIsDisplayed = true;
                return;
            }

            if (password != passwordConfirmation)
            {
                LoaderIsDisplayed = false;
                ErrorMessage = "Пароли не совпадают";
                ErrorIsDisplayed = true;
                return;
            }

            LoaderIsDisplayed = false;

            Switch();
        }
        else
        {
            if (StringHelper.IsNullOrEmpty(firstName, surname))
            {
                LoaderIsDisplayed = true;
                ErrorMessage = "Необходимо заполнить все поля";
                ErrorIsDisplayed = true;
                return;
            }

            //проверяем, что город выбран

            var model = new RegisterModel
            {
                FirstName = firstName,
                Surname = surname,
                Email = email,
                Password = password,
                CityId = Guid.Parse(city)
            };

            var isSuccessful = await accountService.RegisterAsync(model);

            LoaderIsDisplayed = false;

            if (isSuccessful) 
            {
                ErrorMessage = "Не удалось зарегистрироваться";
                ErrorIsDisplayed = true;
            }
            else
            {
                //TODO на вашу почту отправлено письмо...
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
}
