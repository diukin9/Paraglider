using FluentValidation;
using Paraglider.API.Features.Authorization.Commands;
using Paraglider.Web.Definitions.Base;

namespace Paraglider.Web.Definitions.FluentValidating;

public class FluentValidationDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IValidator<BasicAuthRequest>, BasicAuthRequestValidator>();

        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    }
}