using Paraglider.AspNetCore.Identity.Domain.Entities;
using Paraglider.AspNetCore.Identity.Domain.Services;
using Paraglider.AspNetCore.Identity.Domain.Services.Interfaces;
using Paraglider.AspNetCore.Identity.Web.Definitions.Base;

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

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}