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
    public static void SeedUsers(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        using var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

        const string EMAIL = "developer@paraglider.com";
        const string USERNAME = "developer";
        const string PASSWORD = "qwerty123";

        if (userManager!.FindByEmailAsync(EMAIL).Result is not null)
        {
            return;
        }

        var city = context!.Cities
            .Where(x => x.Key == AppData.DefaultCityKey)
            .SingleOrDefault();

        var user = UserFactory.Create(new UserData(
            firstName: "Иван",
            surname: "Иванов",
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

    public static void SeedCities(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        var defaultCity = context!.Cities
            .Where(x => x.Key == AppData.DefaultCityKey)
            .SingleOrDefault();

        if (defaultCity is null)
        {
            defaultCity = CityFactory.Create(new CityData(AppData.DefaultCityName, AppData.DefaultCityKey));
            context!.Add(defaultCity);
            context.SaveChanges();
        }
    }
}