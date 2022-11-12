using Paraglider.Domain.Entities;
using Paraglider.Domain.Services;
using Paraglider.Domain.Services.Interfaces;
using Paraglider.Web.Definitions.Base;

namespace Paraglider.Web.Definitions.DependencyContainer
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