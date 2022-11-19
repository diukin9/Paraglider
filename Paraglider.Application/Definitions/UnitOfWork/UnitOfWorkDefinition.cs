using Paraglider.Data;
using Paraglider.Data.EntityFrameworkCore.Repositories;
using Paraglider.Infrastructure.Common;
using Paraglider.API.Definitions.Base;
using Paraglider.Infrastructure.Common.Abstractions;
using Paraglider.Domain.RDB.Entities;

namespace Paraglider.API.Definitions.UnitOfWork;

public class UnitOfWorkDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRepositoryFactory, UnitOfWork<ApplicationDbContext>>();
        services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();
        services.AddScoped<IUnitOfWork<ApplicationDbContext>, UnitOfWork<ApplicationDbContext>>();

        services.AddScoped<IRepository<City>, CityRepository>();
        services.AddScoped<IRepository<ApplicationUser>, UserRepository>();
    }
}
