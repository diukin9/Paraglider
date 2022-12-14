using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Paraglider.Data.EntityFrameworkCore.Factories;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Exceptions;

namespace Paraglider.Data;

//WARNING: don't create async methods
public static class DatabaseInitializer
{
    public static void Run(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        using var context = scope.ServiceProvider.GetService<ApplicationDbContext>()!;
        using var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>()!;

        SeedCategories(context);
        SeedCities(context);
        SeedDeveloperUser(userManager, context);

        userManager.Dispose();
        context.Dispose();
        scope.Dispose();
    }

    private static void SeedCategories(ApplicationDbContext context)
    {
        var existing = context.Categories.Select(x => x.Name).ToList();

        var categories = new List<Category>()
        {
            new Category() { Name = "Банкетные залы", Keys = new List<ExternalCategoryKey>() { new ExternalCategoryKey() { Source = Source.Gorko, Key = "restaurant" } } },
            new Category() { Name = "Видеографы", Keys = new List<ExternalCategoryKey>() { new ExternalCategoryKey() { Source = Source.Gorko, Key = "video" } } },
            new Category() { Name = "Фотографы", Keys = new List<ExternalCategoryKey>() { new ExternalCategoryKey() { Source = Source.Gorko, Key = "photo" } } },
            new Category() { Name = "Ведущие", Keys = new List<ExternalCategoryKey>() { new ExternalCategoryKey() { Source = Source.Gorko, Key = "tamada" } } },
            new Category() { Name = "Оформители", Keys = new List<ExternalCategoryKey>() { new ExternalCategoryKey() { Source = Source.Gorko, Key = "decorator" } } },
            new Category() { Name = "Службы кейтеринга", Keys = new List<ExternalCategoryKey>() { new ExternalCategoryKey() { Source = Source.Gorko, Key = "catering" } } },
            new Category() { Name = "Выездные регистраторы", Keys = new List<ExternalCategoryKey>() { new ExternalCategoryKey() { Source = Source.Gorko, Key = "ceremony" } } },
            new Category() { Name = "Диджеи", Keys = new List<ExternalCategoryKey>() { new ExternalCategoryKey() { Source = Source.Gorko, Key = "dj" } } },
            new Category() { Name = "Кондитеры", Keys = new List<ExternalCategoryKey>() { new ExternalCategoryKey() { Source = Source.Gorko, Key = "cake" } } },
            new Category() { Name = "Фотостудии", Keys = new List <ExternalCategoryKey>() { new ExternalCategoryKey() { Source = Source.Gorko, Key = "photostudio" } } },
            new Category() { Name = "Лимузины", Keys = new List <ExternalCategoryKey>() { new ExternalCategoryKey() { Source = Source.Gorko, Key = "" } } },
            new Category() { Name = "Свадебные стилисты", Keys = new List <ExternalCategoryKey>() { new ExternalCategoryKey() { Source = Source.Gorko, Key = "style" } } },
            new Category() { Name = "Другое" }
        };

        categories = categories
            .Where(x => !existing.Contains(x.Name))
            .ToList();

        context.Categories.AddRange(categories);
        context.SaveChanges();
    }

    private static void SeedCities(ApplicationDbContext context)
    {
        var existing = context.Cities.Select(x => x.Name).ToList();

        var cities = new List<City>()
        {
            new City() { Name = "Москва", Keys = new List<ExternalCityKey>() { new ExternalCityKey() { Source = Source.Gorko, Key = "4400" } } },
            new City() { Name = "Санкт-Петербург", Keys = new List<ExternalCityKey>() { new ExternalCityKey() { Source = Source.Gorko, Key = "4962" } } },
            new City() { Name = "Екатеринбург", Keys = new List<ExternalCityKey>() { new ExternalCityKey() { Source = Source.Gorko, Key = "5106" } } }
        };

        cities = cities
            .Where(x => !context.Cities.Any(y => y.Name == x.Name))
            .ToList();

        context.Cities.AddRange(cities);
        context.SaveChanges();
    }

    private static void SeedDeveloperUser(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
    {
        const string EMAIL = "developer@paraglider.com";
        const string USERNAME = "developer";
        const string PASSWORD = "qwerty123";

        if (userManager!.FindByEmailAsync(EMAIL).Result is not null)
        {
            return;
        }

        var city = context!.Cities
            .Where(x => x.Name == AppData.DefaultCityName)
            .SingleOrDefault();

        var user = UserFactory.Create(new UserData(
            firstName: "Разработчик",
            surname: "Разработчик",
            username: USERNAME,
            city: city ?? throw new DatabaseInitializerException(),
            email: EMAIL,
            emailConfirmed: true));

        var result = userManager!.CreateAsync(user, PASSWORD).Result;
        if (!result.Succeeded)
        {
            throw new DatabaseInitializerException(string.Join(" ;", result.Errors));
        }
    }
}