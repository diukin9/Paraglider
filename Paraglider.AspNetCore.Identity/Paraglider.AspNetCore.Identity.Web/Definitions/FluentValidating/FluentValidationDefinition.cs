using FluentValidation;
using Paraglider.AspNetCore.Identity.Web.Definitions.Base;
using Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Validators;
using Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.ViewModels;

namespace Paraglider.AspNetCore.Identity.Web.Definitions.FluentValidating
{
    /// <summary>
    /// FluentValidation registration as Application definition
    /// </summary>
    public class FluentValidationDefinition : AppDefinition
    {
        /// <summary>
        /// Configure services for current application
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IValidator<BasicAuthorizeViewModel>, BasicAuthorizeValidator>();

            services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        }
    }
}