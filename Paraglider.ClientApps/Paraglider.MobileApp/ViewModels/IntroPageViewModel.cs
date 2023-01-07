using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Paraglider.MobileApp.Models;
using Paraglider.MobileApp.Pages;
using Paraglider.MobileApp.Services;
using System.Collections.ObjectModel;

namespace Paraglider.MobileApp.ViewModels;

public partial class IntroPageViewModel : BaseViewModel
{
    [ObservableProperty]
    private int position;

    [ObservableProperty]
    private ObservableCollection<IntroScreen> introScreens = new();

    [ObservableProperty]
    private string btnText = "Далее";

    private readonly NavigationService navigationService;

    public IntroPageViewModel(NavigationService navigationService)
    {
        introScreens.Add(new IntroScreen()
        {
            Title = "Добро пожаловать",
            Description = "ПараПлан - конфигуратор свадебных мероприятий, который поможет вам создать план " +
                "вашей свадьбы\n\nДля создания и последующего заполнения плана нужно сделать несколько простых " +
                "действий",
            ImagePath = "carousel_welcome.svg"
        });
        introScreens.Add(new IntroScreen()
        {
            Title = "Выбор категорий",
            Description = "Выберите категории товаров и услуг, в которых вы планируете найти исполнителей",
            ImagePath = "carousel_select_categories.svg"
        });
        introScreens.Add(new IntroScreen()
        {
            Title = "Поиск исполнителя",
            Description = "Найти подходящего исполнителя в выбранной категории по стоимости, отзывам и прочим " +
                "параметрам",
            ImagePath = "carousel_find_performer.svg"
        });
        introScreens.Add(new IntroScreen()
        {
            Title = "Связь с исполнителем",
            Description = "Вы можете связаться с выбранным исполнителем через мессенджеры или по номеру телефона",
            ImagePath = "carousel_link_with_performer.svg"
        });
        introScreens.Add(new IntroScreen()
        {
            Title = "Готовый план",
            Description = "После выбора исполнителей вы получите готовый план мероприятия с итоговой стоимостью " +
                "свадьбы",
            ImagePath = "carousel_ready_plan.svg"
        });

        this.navigationService = navigationService;
    }

    [RelayCommand]
    private void OnPositionChanged()
    {
        BtnText = position == introScreens.Count - 1 ? "Начать" : "Далее";
    }

    [RelayCommand]
    private async Task OnBtnClickedAsync()
    {
        if (IsBusy) return;

        IsBusy = true;

        if (position < introScreens.Count - 1) Position++;
        else await navigationService.GoToAsync<MainPage>(true);

        IsBusy = false;
    }
}
