using Paraglider.Data;
using Paraglider.API.Definitions.Base;

namespace Paraglider.API.Definitions.DataSeeding;

public class DataSeedingDefinition : AppDefinition
{
    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        DatabaseInitializer.Run(app.Services);
    }
}