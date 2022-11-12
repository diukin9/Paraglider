using FluentValidation;
using Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.ViewModels;

namespace Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Queries.Validators
{
    public class PostBasicAuthRequestValidator : AbstractValidator<BasicAuthViewModel>
    {
        public PostBasicAuthRequestValidator() => RuleSet("default", () =>
        {
            RuleFor(x => x.Login).NotNull().NotEmpty().MinimumLength(3).MaximumLength(64);
            RuleFor(x => x.Password).MinimumLength(8).MaximumLength(64);
        });
    }
}
