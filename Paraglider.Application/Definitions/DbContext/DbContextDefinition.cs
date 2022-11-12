using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Paraglider.Data;
using Paraglider.Domain;
using Paraglider.Domain.Entities;
using Paraglider.Web.Definitions.Base;

namespace Paraglider.Web.Definitions.DbContext
{
    /// <summary>
    /// ASP.NET Core services registration and configurations
    /// </summary>
    public class DbContextDefinition : AppDefinition
    {
        /// <summary>
        /// Configure services for current application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(config =>
            {
                config.UseNpgsql("Host=localhost;Port=5432;Database=paragliderDb;Username=postgres;Password=2Hi7go22;Pooling=true;");
            });

            services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequireLowercase = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
                config.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IdentityErrorDescriber>();
        }
    }
}