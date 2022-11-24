using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Paraglider.Data.EntityFrameworkCore.Factories;
using Paraglider.Domain.RDB.Entities;
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
            new Category() { Name = "Банкетные залы" },
            new Category() { Name = "Видеографы" },
            new Category() { Name = "Фотографы" },
            new Category() { Name = "Ведущие" },
            new Category() { Name = "Оформители" },
            new Category() { Name = "Службы кейтеринга" },
            new Category() { Name = "Выездные регистраторы" },
            new Category() { Name = "Диджеи" },
            new Category() { Name = "Кондитеры" },
            new Category() { Name = "Фотостудии" },
            new Category() { Name = "Лимузины" },
            new Category() { Name = "Свадебные стилисты" },
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
            new City() { Name = "Москва" },
            new City() { Name = "Санкт-Петербург",  },
            new City() { Name = "Екатеринбург" }
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