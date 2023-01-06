using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Application.Extensions;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Helpers;
using Paraglider.Infrastructure.Common.Models;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Paraglider.Application.Features.Auth.Commands;

public class GetTokenRequest : IRequest<OperationResult<Infrastructure.Common.Models.Token>>
{
    [Required]
    [JsonPropertyName("login")]
    public string Login { get; set; } = null!;

    [Required]
    [JsonPropertyName("password")]
    public string Password { get; set; } = null!;
}

public class GetTokenCommandHandler 
    : IRequestHandler<GetTokenRequest, OperationResult<Infrastructure.Common.Models.Token>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly BearerSettings _bearerSettings;

    public GetTokenCommandHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        BearerSettings bearerSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _bearerSettings = bearerSettings;
    }

    public async Task<OperationResult<Infrastructure.Common.Models.Token>> Handle(
        GetTokenRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult<Infrastructure.Common.Models.Token>();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //получаем пользователя
        var user = await _userManager.FindByNameIdentifierAsync(request.Login);
        if (user is null)
        {
            return operation.AddError("Неверный логин или пароль");
        }

        var checkPasswordResult = await _signInManager
            .CheckPasswordSignInAsync(user, request.Password, false);

        //проверяем, что передан верный пароль
        if (!checkPasswordResult.Succeeded)
        {
            return operation.AddError("Неверный логин или пароль");
        }

        //прокинем необходимые клеймы для будушей генерации токена
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, request.Login),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var now = DateTime.UtcNow;

        //сгенерируем access_token
        var accessTokenExpiryTime = now.AddSeconds(_bearerSettings.AccessTokenLifetimeInSeconds);
        var accessToken = TokenHelper.GenerateAccessToken(
            key: _bearerSettings.Key,
            issuer: _bearerSettings.Issuer,
            audience: _bearerSettings.Audience,
            expires: accessTokenExpiryTime,
            claims: claims);

        //если у пользователя нет refresh_token или он просрочен - создаем новый
        if (user.RefreshToken is null || user.RefreshTokenExpiryTime < now)
        {
            //сгенерируем refresh_token
            var refreshToken = TokenHelper.GenerateRefreshToken();
            var refreshTokenExpiryTime = now.AddSeconds(_bearerSettings.RefreshTokenLifetimeInSeconds);

            //сохраним refresh_token внутри пользователя
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = refreshTokenExpiryTime;
            await _userManager.UpdateAsync(user);
        }

        var token = new Infrastructure.Common.Models.Token()
        {
            AccessToken = accessToken,
            AccessTokenExpiryTime = accessTokenExpiryTime,
            RefreshToken = user.RefreshToken,
            RefreshTokenExpiryTime = user.RefreshTokenExpiryTime,
        };

        return operation.AddSuccess(string.Empty, token);
    }
}