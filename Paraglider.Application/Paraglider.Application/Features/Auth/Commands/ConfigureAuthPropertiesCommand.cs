using System.ComponentModel.DataAnnotations;
using System.Net;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Attributes;
using Paraglider.Infrastructure.Common.Enums;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Application.Features.Auth.Commands;

public class ConfigureAuthPropertiesRequest 
    : IRequest<OperationResult<AuthenticationProperties>>
{
    [Required, IsEnumName(typeof(AuthProvider))] 
    public string Provider { get; set; } = null!;

    public string? Callback { get; set; } = null!;

    [Required]
    public AuthType AuthType { get; set; }

    public ConfigureAuthPropertiesRequest(
        string provider, 
        AuthType authType, 
        string? callback = null)
    {
        Provider = provider;
        Callback = callback;
        AuthType = authType;
    }
}

public class ConfigureAuthPropertiesCommandHandler 
    : IRequestHandler<ConfigureAuthPropertiesRequest, 
        OperationResult<AuthenticationProperties>>
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public ConfigureAuthPropertiesCommandHandler(
        SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public Task<OperationResult<AuthenticationProperties>> Handle(
        ConfigureAuthPropertiesRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult<AuthenticationProperties>();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return Task.FromResult(operation.AddError(validation));

        //конфигурируем authentication properties
        var callbackUrl = $"{DomainUrl}{ExternalAuthHandlerRelativePath}" +
                          $"?authType={(int)request.AuthType}";

        if (request.AuthType == AuthType.Cookie)
        {
            callbackUrl += $"&callback={WebUtility.UrlEncode(request.Callback)}";
        }

        var properties = _signInManager.ConfigureExternalAuthenticationProperties(request.Provider, callbackUrl);

        return Task.FromResult(operation.AddSuccess(string.Empty, properties));
    }
}
