using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Paraglider.API.Definitions.Base;
using Paraglider.Data;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.API.Definitions.DbContext;

public class DbContextDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(config =>
        {
            config.UseNpgsql(configuration["ConnectionStrings:DefaultConnection"]);
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