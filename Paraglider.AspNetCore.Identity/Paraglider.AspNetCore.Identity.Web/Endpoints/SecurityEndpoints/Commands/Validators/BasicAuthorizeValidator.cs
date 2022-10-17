using FluentValidation;
using Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.ViewModels;

namespace Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Validators
{
    public class BasicAuthorizeValidator : AbstractValidator<BasicAuthorizeViewModel>
    {
        public BasicAuthorizeValidator() => RuleSet("default", () =>
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().MinimumLength(3).MaximumLength(64).EmailAddress();
            RuleFor(x => x.Password).MinimumLength(8).MaximumLength(64);
        });
    }
}
