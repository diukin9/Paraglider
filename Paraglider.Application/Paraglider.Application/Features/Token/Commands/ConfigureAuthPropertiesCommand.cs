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

namespace Paraglider.Application.Features.Token.Commands;

public class ConfigureAuthPropertiesRequest 
    : IRequest<OperationResult<AuthenticationProperties>>
{
    [Required, IsEnumName(typeof(AuthProvider))] 
    public string Provider { get; set; } = null!;

    [Required] 
    public string Callback { get; set; } = null!;

    public ConfigureAuthPropertiesRequest(string provider, string returnUrl)
    {
        Provider = provider;
        Callback = returnUrl;
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
        request.Callback = WebUtility.UrlEncode(request.Callback);
        var callbackUrl = $"{ExternalAuthHandlerRelativePath}?callback={request.Callback}";
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(request.Provider, callbackUrl);

        return Task.FromResult(operation.AddSuccess(string.Empty, properties));
    }
}
