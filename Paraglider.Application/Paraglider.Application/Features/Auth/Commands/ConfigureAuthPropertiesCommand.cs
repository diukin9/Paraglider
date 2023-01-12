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
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Application.Features.Auth.Commands;

public class ConfigureAuthPropertiesRequest 
    : IRequest<InternalOperation<AuthenticationProperties>>
{
    [Required, IsEnumName(typeof(AuthProvider))]
    [JsonPropertyName("provider")]
    public string Provider { get; set; } = null!;

    [JsonPropertyName("callback_url")]
    public string? Callback { get; set; } = null!;

    [Required]
    [JsonPropertyName("auth_scheme")]
    public AuthScheme Scheme { get; set; }

    public ConfigureAuthPropertiesRequest(
        string provider, 
        AuthScheme scheme, 
        string? callback = null)
    {
        Provider = provider;
        Callback = callback;
        Scheme = scheme;
    }
}

public class ConfigureAuthPropertiesCommandHandler 
    : IRequestHandler<ConfigureAuthPropertiesRequest, 
        InternalOperation<AuthenticationProperties>>
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public ConfigureAuthPropertiesCommandHandler(
        SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public Task<InternalOperation<AuthenticationProperties>> Handle(
        ConfigureAuthPropertiesRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new InternalOperation<AuthenticationProperties>();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return Task.FromResult(operation.AddError(validation));

        //конфигурируем authentication properties
        var callbackUrl = $"{ExternalAuthHandlerRelativePath}?authType={(int)request.Scheme}";

        if (request.Scheme == AuthScheme.Cookie)
        {
            callbackUrl += $"&callback={WebUtility.UrlEncode(request.Callback)}";
        }

        var properties = _signInManager.ConfigureExternalAuthenticationProperties(request.Provider, callbackUrl);

        return Task.FromResult(operation.AddSuccess(properties));
    }
}
