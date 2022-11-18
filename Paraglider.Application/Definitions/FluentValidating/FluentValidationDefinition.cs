using FluentValidation;
using Paraglider.API.Features.Authorization.Commands;
using Paraglider.Web.Definitions.Base;
using System.Globalization;

namespace Paraglider.Web.Definitions.FluentValidating;

public class FluentValidationDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IValidator<BasicAuthRequest>, BasicAuthRequestValidator>();
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("en");
        services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    }
}