using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Paraglider.Domain.Entities;
using Paraglider.Infrastructure.Exceptions;

namespace Paraglider.Data;

public static class DatabaseInitializer
{
    public static async void SeedUsers(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        await using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
        using var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

        const string EMAIL = "developer@paraglider.com";
        const string USERNAME = "developer";
        const string PASSWORD = "qwerty123";

        if (userManager!.FindByEmailAsync(EMAIL).Result is not null)
        {
            return;
        }

        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = USERNAME,
            Email = EMAIL,
            EmailConfirmed = true,
            Surname = "Иванов",
            FirstName = "Иван",
            City = new City()
            {
                Name = "Екатеринбург",
                Key = "ekb",
            }
        };

        var result = await userManager!.CreateAsync(user, PASSWORD);
        if (!result.Succeeded)
        {
            throw new DatabaseInitializerException(string.Join(" ;", result.Errors));
        }
    }
}