using MediatR;
using Paraglider.Web.Definitions.Base;
using System.Reflection;

namespace Paraglider.Web.Definitions.Mediator
{
    /// <summary>
    /// Register Mediator as application definition
    /// </summary>
    public class MediatorDefinition : AppDefinition
    {
        /// <summary>
        /// Configure services for current application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}