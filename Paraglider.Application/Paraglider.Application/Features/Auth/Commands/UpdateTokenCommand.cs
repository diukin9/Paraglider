using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Paraglider.Application.Extensions;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Helpers;
using Paraglider.Infrastructure.Common.Models;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Paraglider.Application.Features.Auth.Commands;

public class RefreshTokenRequest : IRequest<OperationResult<Token>>
{
    [Required]
    [JsonPropertyName("expired_access_token")]
    public string ExpiredAccessToken { get; set; } = null!;

    [Required]
    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; } = null!;
}

public class RefreshTokenCommandHandler 
    : IRequestHandler<RefreshTokenRequest, OperationResult<Token>>
{
    private readonly TokenValidationParameters _validationParameters;
    private readonly BearerSettings _bearerSettings;
    private readonly IHttpContextAccessor _accessor;
    private readonly UserManager<ApplicationUser> _userManager;

    public RefreshTokenCommandHandler(
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor accessor,
        BearerSettings bearerSettings,
        TokenValidationParameters validationParameters)
    {
        _bearerSettings = bearerSettings;
        _validationParameters = validationParameters;
        _accessor = accessor;
        _userManager = userManager;
    }

    public async Task<OperationResult<Token>> Handle(
        RefreshTokenRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult<Token>();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //получим ClaimsPrincipal текущего пользователя через access_token
        var principal = TokenHelper.GetPrincipalFromExpiredToken(
            accessToken: request.ExpiredAccessToken, 
            validationParameters: _validationParameters);

        if (principal is null)
        {
            return operation.AddError("Неверный access_token или refresh_token");
        }

        //вытащим логин/почту пользователя из токена
        var identifier = _accessor.HttpContext!.Request.Headers.GetNameIdentifierFromBearerToken();
        if (string.IsNullOrEmpty(identifier))
        {
            return operation.AddError("Не удалось аутентифицировать пользователя");
        }

        //получим через логин/почту пользователя
        var user = await _userManager.FindByNameIdentifierAsync(identifier!);
        if (user is null)
        {
            return operation.AddError("Не удалось аутентифицировать пользователя");
        }

        //проверим валидность refresh_token
        if (user.RefreshToken != request.RefreshToken) 
        {
            return operation.AddError("Неверный access_token или refresh_token");
        }

        var now = DateTime.UtcNow;

        //если refresh_token просрочился
        if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
        {
            return operation.AddError("У refresh_token истек срок действия");
        }

        //сгенерируем новый access_token
        var accessTokenExpiryTime = now.AddSeconds(_bearerSettings.AccessTokenLifetimeInSeconds);
        var newAccessToken = TokenHelper.GenerateAccessToken(
            key: _bearerSettings.Key,
            issuer: _bearerSettings.Issuer,
            audience: _bearerSettings.Audience,
            expires: accessTokenExpiryTime,
            claims: principal.Claims.ToList());

        var token = new Token()
        {
            AccessToken = newAccessToken,
            AccessTokenExpiryTime = accessTokenExpiryTime,
            RefreshToken = user.RefreshToken,
            RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
        };

        return operation.AddSuccess(string.Empty, token);
    }
}
