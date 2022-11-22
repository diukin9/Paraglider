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
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.Definitions.DbContext;

public class DatabasesDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(config =>
        {
            config.UseNpgsql(configuration["ConnectionStrings:PostgreSQL"]);
        }).AddIdentity<ApplicationUser, ApplicationRole>(config =>
        {
            config.Password.RequireDigit = false;
            config.Password.RequireLowercase = false;
            config.Password.RequireNonAlphanumeric = false;
            config.Password.RequireUppercase = false;
            config.Password.RequiredLength = 8;
        }).AddEntityFrameworkStores<ApplicationDbContext>();

        services.AddTransient<IdentityErrorDescriber>();

        var mongoSettings = new MongoDbSettings();
        configuration.Bind(nameof(MongoDbSettings), mongoSettings);
        services.AddSingleton<IMongoDbSettings>(mongoSettings);

        var mongoClient = new MongoClient(configuration["ConnectionStrings:MongoDB"]);
        services.AddSingleton<IMongoClient>(mongoClient);
    }
}