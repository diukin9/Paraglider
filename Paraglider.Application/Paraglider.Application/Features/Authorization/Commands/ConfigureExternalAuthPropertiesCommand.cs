using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Attributes;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using System.Net;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Application.Features.Authorization.Commands;

public class ConfigureExternalAuthPropertiesRequest : IRequest<OperationResult<AuthenticationProperties>>
{
    [Required, IsEnumName(typeof(AuthProvider))] public string Provider { get; set; } = null!;
    [Required] public string ReturnUrl { get; set; } = null!;

    public ConfigureExternalAuthPropertiesRequest(string provider, string returnUrl)
    {
        Provider = provider;
        ReturnUrl = returnUrl;
    }
}

public class ConfigureExternalAuthPropertiesCommandHandler 
    : IRequestHandler<ConfigureExternalAuthPropertiesRequest, 
        OperationResult<AuthenticationProperties>>
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public ConfigureExternalAuthPropertiesCommandHandler(
        SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public Task<OperationResult<AuthenticationProperties>> Handle(
        ConfigureExternalAuthPropertiesRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult<AuthenticationProperties>();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return Task.FromResult(operation.AddError(validation));

        //конфигурируем authentication properties
        request.ReturnUrl = WebUtility.UrlEncode(request.ReturnUrl);
        var callbackUrl = $"{ExternalAuthHandlerRelativePath}?returnUrl={request.ReturnUrl}";
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(request.Provider, callbackUrl);

        return Task.FromResult(operation.AddSuccess(string.Empty, properties));
    }
}
