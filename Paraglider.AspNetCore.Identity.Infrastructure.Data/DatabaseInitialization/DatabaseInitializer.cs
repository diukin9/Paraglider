using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Paraglider.AspNetCore.Identity.Infrastructure.Data.DatabaseInitialization
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
            const string PASSWORD = "qwerty123";

            if (userManager!.FindByEmailAsync(EMAIL).Result != null)
            {
                return;
            }

            var user = new ApplicationUser()
            {
                UserName = EMAIL,
                Email = EMAIL,
                EmailConfirmed = true,
                FirstName = "Иван",
                Surname = "Иванов",
                SecondName = "Иванович"
            };

            var result = await userManager!.CreateAsync(user, PASSWORD);
            if (!result.Succeeded)
            {
                //some logics here..
            }
        }
    }
}