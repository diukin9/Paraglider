using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Domain.Entities;
using Paraglider.Domain.Enums;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using System.Net;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Authorization.Commands
{
    public class ConfigureExternalAuthPropertiesRequest : IRequest<OperationResult>
    {
        public string Provider { get; set; } = null!;
        public string? ReturnUrl { get; set; }

        public ConfigureExternalAuthPropertiesRequest(string provider, string? returnUrl = null)
        {
            Provider = provider;
            ReturnUrl = returnUrl;
        }

        public class ConfigureExternalAuthPropertiesRequestValidator 
            : AbstractValidator<ConfigureExternalAuthPropertiesRequest>
        {
            public ConfigureExternalAuthPropertiesRequestValidator() => RuleSet(DefaultRuleSetName, () =>
            {
                RuleFor(x => x.Provider).IsEnumName(typeof(ExternalAuthProvider), false);
                RuleFor(x => x.ReturnUrl).NotEmpty().NotNull();
            });
        }
    }

    public class ConfigureExternalAuthPropertiesCommandHandler 
        : IRequestHandler<ConfigureExternalAuthPropertiesRequest, OperationResult>
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IValidator<ConfigureExternalAuthPropertiesRequest> _validator;

        public ConfigureExternalAuthPropertiesCommandHandler(
            SignInManager<ApplicationUser> signInManager,
            IValidator<ConfigureExternalAuthPropertiesRequest> validator)
        {
            _signInManager = signInManager;
            _validator = validator;
        }

        public async Task<OperationResult> Handle(
            ConfigureExternalAuthPropertiesRequest request,
            CancellationToken cancellationToken)
        {
            var operation = new OperationResult();

            var validateResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validateResult.IsValid)
            {
                return operation.AddError(string.Join("; ", validateResult.Errors));
            }

            request.ReturnUrl = WebUtility.UrlEncode(request.ReturnUrl);
            var callbackUrl = $"{ExternalAuthHandlerRelativePath}?returnUrl={request.ReturnUrl}";
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(request.Provider, callbackUrl);

            return operation.AddSuccess(string.Empty, properties);
        }
    }
}
