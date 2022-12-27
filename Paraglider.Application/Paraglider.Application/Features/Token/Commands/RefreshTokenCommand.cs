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
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Application.Features.Token.Commands;

public class RefreshTokenRequest : IRequest<OperationResult<Tokens>>
{
    [Required]
    public string AccessToken { get; set; } = null!;

    [Required]
    public string RefreshToken { get; set; } = null!;
}

public class RefreshTokenCommandHandler 
    : IRequestHandler<RefreshTokenRequest, OperationResult<Tokens>>
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

    public async Task<OperationResult<Tokens>> Handle(
        RefreshTokenRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult<Tokens>();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //получим ClaimsPrincipal текущего пользователя через access_token
        var principal = TokenHelper.GetPrincipalFromExpiredToken(
            accessToken: request.AccessToken, 
            validationParameters: _validationParameters);

        if (principal is null)
        {
            return operation.AddError("Неверный access_token или refresh_token");
        }

        //вытащим логин/почту пользователя из токена
        var identifier = _accessor.HttpContext!.Request.Headers.GetNameIdentifierFromBearerToken();
        if (string.IsNullOrEmpty(identifier))
        {
            return operation.AddError("Логин/почтовый ящик пользователя был пуст");
        }

        //получим через логин/почту пользователя
        var user = await _userManager.FindByNameIdentifierAsync(identifier!);
        if (user is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(ApplicationUser)));
        }

        //проверим валидность refresh_token
        if (user.RefreshToken != request.RefreshToken 
            || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return operation.AddError("Неверный access_token или refresh_token");
        }

        //сгенерируем новый access_token
        var newAccessToken = TokenHelper.GenerateAccessToken(
            key: _bearerSettings.Key,
            issuer: _bearerSettings.Issuer,
            audience: _bearerSettings.Audience,
            expires: DateTime.UtcNow.AddSeconds(_bearerSettings.AccessTokenLifetimeInSeconds),
            claims: principal.Claims.ToList());

        //сгенерируем новый refresh_token
        var newRefreshToken = TokenHelper.GenerateRefreshToken();

        //обновим refresh_token пользователя
        user.RefreshToken = newRefreshToken;
        await _userManager.UpdateAsync(user);

        var token = new Infrastructure.Common.Models.Tokens()
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken,
            Expiration = user.RefreshTokenExpiryTime
        };

        return operation.AddSuccess(string.Empty, token);
    }
}
