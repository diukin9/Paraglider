using Paraglider.AspNetCore.Identity.Web.Application.Services;
using Paraglider.AspNetCore.Identity.Web.Definitions.Base;
using Paraglider.AspNetCore.Identity.Web.Definitions.Identity;

namespace Paraglider.AspNetCore.Identity.Web.Definitions.DependencyContainer
{
    /// <summary>
    /// Dependency container definition
    /// </summary>
    public class ContainerDefinition : AppDefinition
    {
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);
            services.AddTransient<IAccountService, AccountService>();
        }
    }
}