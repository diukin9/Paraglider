using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Paraglider.API.Definitions.Base;
using Paraglider.Data;
using Paraglider.Data.MongoDB;
using Paraglider.Domain.NoSQL.Entities;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.API.Definitions.DbContext;

public class DatabasesDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(config =>
        {
            config.UseNpgsql(configuration["ConnectionStrings:PostgreSQL"]);
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

        var settings = new MongoDbSettings();
        configuration.Bind(nameof(MongoDbSettings), settings);
        services.AddSingleton<IMongoDbSettings>(settings);

        services.AddSingleton<IMongoClient>(new MongoClient(configuration["ConnectionStrings:MongoDB"]));
        services.AddScoped<IDataAccess<WeddingComponent>, WeddingComponentDataAccess>();
    }
}