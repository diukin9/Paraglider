using FluentValidation;
using Paraglider.Web.Definitions.Base;
using Paraglider.Web.Endpoints.SecurityEndpoints.Queries.Validators;
using Paraglider.Web.Endpoints.SecurityEndpoints.ViewModels;

namespace Paraglider.Web.Definitions.FluentValidating
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
            services.AddScoped<IValidator<BasicAuthViewModel>, PostBasicAuthRequestValidator>();

            services.AddValidatorsFromAssembly(typeof(Program).Assembly);
        }
    }
}