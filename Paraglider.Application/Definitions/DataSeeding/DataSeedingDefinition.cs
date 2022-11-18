using Paraglider.Data;
using Paraglider.Web.Definitions.Base;

namespace Paraglider.Web.Definitions.DataSeeding;

public class DataSeedingDefinition : AppDefinition
{
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        DatabaseInitializer.SeedCities(app.Services);
        DatabaseInitializer.SeedUsers(app.Services);
    }
}