using Microsoft.EntityFrameworkCore;
using Paraglider.Data;
using Paraglider.API.Definitions.Base;

namespace Paraglider.API.Definitions.DataSeeding;

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