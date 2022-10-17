using Paraglider.AspNetCore.Identity.Infrastructure.DatabaseInitialization;
using Paraglider.AspNetCore.Identity.Web.Definitions.Base;

namespace Paraglider.AspNetCore.Identity.Web.Definitions.DataSeeding
{
    /// <summary>
    /// Seeding DbContext for default data for EntityFrameworkCore
    /// </summary>
    public class DataSeedingDefinition : AppDefinition
    {
        /// <summary>
        /// Configure application for current application
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
        {
            DatabaseInitializer.SeedUsers(app.Services);
        }
    }
}