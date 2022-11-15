using Paraglider.Data;
using Paraglider.Data.Repositories;
using Paraglider.Domain.Entities;
using Paraglider.Infrastructure;
using Paraglider.Web.Definitions.Base;

namespace Paraglider.API.Definitions.UnitOfWork;

public class UnitOfWorkDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IRepositoryFactory, UnitOfWork<ApplicationDbContext>>();
        services.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();
        services.AddScoped<IUnitOfWork<ApplicationDbContext>, UnitOfWork<ApplicationDbContext>>();

        services.AddScoped<IRepository<ApplicationUser>, UserRepository>();
    }
}
