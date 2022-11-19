using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Paraglider.Domain.Entities;
using Paraglider.Domain.Enums;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Extensions;
using System.Security.Claims;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Authorization.Commands
{
    public class ExternalAuthRequest : IRequest<OperationResult>
    {
        public ExternalLoginInfo Info { get; set; }

        public ExternalAuthRequest(ExternalLoginInfo info)
        {
            Info = info;
        }
    }

    public class ExternalAuthRequestValidator : AbstractValidator<ExternalAuthRequest>
    {
        public ExternalAuthRequestValidator() => RuleSet(DefaultRuleSetName, () =>
        {
            RuleFor(x => x.Info).NotNull();
            RuleFor(x => x.Info.LoginProvider).IsEnumName(typeof(ExternalAuthProvider), false);
        });
    }

    public class ExternalAuthCommandHandler : IRequestHandler<ExternalAuthRequest, OperationResult>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IValidator<ExternalAuthRequest> _validator;

        public ExternalAuthCommandHandler(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IValidator<ExternalAuthRequest> validator)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _validator = validator;
        }

        public async Task<OperationResult> Handle(ExternalAuthRequest request, CancellationToken cancellationToken)
        {
            var operation = new OperationResult();

            var validateResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validateResult.IsValid)
            {
                return operation.AddError(string.Join("; ", validateResult.Errors));
            }

            var provider = Enum.Parse<ExternalAuthProvider>(request.Info.LoginProvider);

            var externalId = request.Info.Principal.Claims.GetByClaimType(ClaimTypes.NameIdentifier);

            var user = await _userManager.Users.Include(x => x.ExternalAuthInfo)
                .Where(x => x.ExternalAuthInfo
                    .Any(y => y.ExternalProvider == provider && y.ExternalId == externalId))
                .SingleOrDefaultAsync();

            if (user is null)
            {
                return operation.AddError(ExceptionMessages.ObjectIsNull(typeof(ApplicationUser)));
            }

            var logins = await _userManager.GetLoginsAsync(user);
            if (!logins.Any(x => x.LoginProvider == request.Info.LoginProvider && x.ProviderKey == externalId))
            {
                var identityResult = await _userManager.AddLoginAsync(user, request.Info);
                if (!identityResult.Succeeded)
                {
                    return operation.AddError(string.Join("; ", identityResult.Errors));
                }
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(
                request.Info.LoginProvider,
                request.Info.ProviderKey,
                true);

            if (!signInResult.Succeeded)
            {
                return operation.AddError(ExceptionMessages.FailedExternalAuth);
            }

            return operation.AddSuccess(Messages.SuccessfullExternalAuth);
        }
    }
}