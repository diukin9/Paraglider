using System.Net;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Authorization.Commands;

public record ConfigureExternalAuthPropertiesRequest(string Provider, string ReturnUrl) 
    : IRequest<OperationResult<AuthenticationProperties>>
{
    public string Provider { get; set; } = Provider;
    public string ReturnUrl { get; set; } = ReturnUrl;
}

public class ConfigureExternalAuthPropertiesRequestValidator 
    : AbstractValidator<ConfigureExternalAuthPropertiesRequest>
{
    public ConfigureExternalAuthPropertiesRequestValidator() => RuleSet(DefaultRuleSetName, () =>
    {
        RuleFor(x => x.Provider).IsEnumName(typeof(AuthProvider), false);
        RuleFor(x => x.ReturnUrl).NotEmpty().NotNull();
    });
}

public class ConfigureExternalAuthPropertiesCommandHandler 
    : IRequestHandler<ConfigureExternalAuthPropertiesRequest, OperationResult<AuthenticationProperties>>
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

    public async Task<OperationResult<AuthenticationProperties>> Handle(
        ConfigureExternalAuthPropertiesRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult<AuthenticationProperties>();

        //валидируем полученные данные
        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return operation.AddError(validateResult.Errors);
        }

        //конфигурируем authentication properties
        request.ReturnUrl = WebUtility.UrlEncode(request.ReturnUrl);
        var callbackUrl = $"{ExternalAuthHandlerRelativePath}?returnUrl={request.ReturnUrl}";
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(request.Provider, callbackUrl);

        return operation.AddSuccess(string.Empty, properties);
    }
}
