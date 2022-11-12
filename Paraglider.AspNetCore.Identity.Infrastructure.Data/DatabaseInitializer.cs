using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Paraglider.AspNetCore.Identity.Domain.Entities;
using Paraglider.AspNetCore.Identity.Infrastructure.Exceptions;

namespace Paraglider.AspNetCore.Identity.Domain
{
    /// <summary>
    /// Database Initializer
    /// </summary>
    public static class DatabaseInitializer
    {
        /// <summary>
        /// Seeds one default users to database for demo purposes only
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <exception cref="InvalidOperationException"></exception>
        public static async void SeedUsers(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            await using var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            using var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            const string EMAIL = "developer@paraglider.com";
            const string USERNAME = "developer";
            const string PASSWORD = "qwerty123";

            if (userManager!.FindByEmailAsync(EMAIL).Result != null)
            {
                return;
            }

            var user = new ApplicationUser
            {
                UserName = USERNAME,
                Email = EMAIL,
                EmailConfirmed = true,
                Surname = "Иванов",
                FirstName = "Иван"
            };

            var result = await userManager!.CreateAsync(user, PASSWORD);
            if (!result.Succeeded)
            {
                throw new DatabaseInitializerException(string.Join(" ;", result.Errors));
            }
        }
    }
}