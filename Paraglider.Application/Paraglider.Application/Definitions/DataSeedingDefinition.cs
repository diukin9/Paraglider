using Microsoft.EntityFrameworkCore;
using Paraglider.Infrastructure.Common.AppDefinition;
using Paraglider.Data.EntityFrameworkCore;

namespace Paraglider.Application.Definitions;

public class DataSeedingDefinition : AppDefinition
{
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        using (var scope = app.Services.CreateScope())
        {
            scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();
        }

        DatabaseInitializer.Run(app.Services);
    }
}