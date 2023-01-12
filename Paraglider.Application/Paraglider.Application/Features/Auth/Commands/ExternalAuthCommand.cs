using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Paraglider.Application.Extensions;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Enums;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Helpers;
using Paraglider.Infrastructure.Common.Models;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace Paraglider.Application.Features.Auth.Commands;

public class ExternalAuthRequest : IRequest<InternalOperation<string>>
{
    [Required]
    [JsonPropertyName("callback_url")]
    public string Callback { get; set; } = null!;

    [Required]
    [JsonPropertyName("auth_scheme")]
    public AuthScheme Scheme { get; set; }
}

public class ExternalAuthCommandHandler 
    : IRequestHandler<ExternalAuthRequest, InternalOperation<string>>
{
    private readonly IHttpContextAccessor _accessor;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly BearerSettings _bearerSettings;

    public ExternalAuthCommandHandler(
        IHttpContextAccessor accessor,
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager, 
        BearerSettings bearerSettings)
    {
        _accessor = accessor;
        _signInManager = signInManager;
        _userManager = userManager;
        _bearerSettings = bearerSettings;
    }

    public async Task<InternalOperation<string>> Handle(
        ExternalAuthRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new InternalOperation<string>();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //получаем ExternalLoginInfo
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info is null) return operation.AddError("Не удалось получить данные пользователя");

        //получаем пользователя
        var user = await GetUserByExternalLoginInfoAsync(info);
        if (user is null) return operation.AddError("Пользователь не найден");

        //если у пользователя нет такого UserLoginInfo - создаем
        if (await _userManager.FindUserLoginInfoAsync(user, info.LoginProvider, info.ProviderKey) is null)
        { 
            var identityResult = await _userManager.AddLoginAsync(user, info);
            if (!identityResult.Succeeded)
            {
                return operation.AddError(string.Join("; ", identityResult.Errors));
            }
        }

        return request.Scheme == AuthScheme.Cookie
            ? await CookieAuthAsync(operation, info, user, request.Callback)
            : await TokenAuthAsync(operation, user, request.Callback);
    }

    private async Task<InternalOperation<string>> CookieAuthAsync(
        InternalOperation<string> operation,
        ExternalLoginInfo info,
        ApplicationUser user,
        string callback)
    {
        var signInResult = await _signInManager.ExternalLoginSignInAsync(
            info.LoginProvider,
            info.ProviderKey,
            true);

        if (!signInResult.Succeeded) return operation.AddError("Ошибка при попытке авторизации");

        await _accessor.HttpContext!.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            ClaimsPrincipalHelper.CreateByUser<ApplicationUser, Guid>(user),
            new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(14)
            });

        return operation.AddSuccess(callback);
    }

    private async Task<InternalOperation<string>> TokenAuthAsync(
        InternalOperation<string> operation,
        ApplicationUser user,
        string callback)
    {
        //если у пользователя не прокинут refresh_token - прокидываем
        if (user.RefreshToken is null) await AssignToUserRefreshTokenAsync(user);

        var accessTokenExpiryTime = DateTime.UtcNow.AddSeconds(_bearerSettings.AccessTokenLifetimeInSeconds);
        //получим access_token
        var accessToken = GetAccessToken(user);

        //преобразуем callback
        var url = BuildCallback(
            $"{callback://}",
            accessToken,
            accessTokenExpiryTime,
            user.RefreshToken!,
            user.RefreshTokenExpiryTime!.Value);

        return operation.AddSuccess(url);
    }

    private async Task<ApplicationUser?> GetUserByExternalLoginInfoAsync(ExternalLoginInfo info)
    {
        //получаем провайдера и внешний id
        var provider = Enum.Parse<AuthProvider>(info.LoginProvider);
        var externalId = info.Principal.Claims.GetByClaimType(ClaimTypes.NameIdentifier);

        //получаем пользователя на основе ExternalLoginInfo
        return await _userManager.FindByExternalLoginInfoAsync(provider, externalId!);
    }

    private async Task AssignToUserRefreshTokenAsync(ApplicationUser user)
    {
        var seconds = _bearerSettings.RefreshTokenLifetimeInSeconds;
        var expiryTime = DateTime.UtcNow.AddSeconds(seconds);

        user.RefreshToken = TokenHelper.GenerateRefreshToken();
        user.RefreshTokenExpiryTime = expiryTime;

        await _userManager.UpdateAsync(user);
    }

    private string GetAccessToken(ApplicationUser user)
    {
        //прокинем необходимые клеймы для будушей генерации токена
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        //сгенерируем access_token
        var accessToken = TokenHelper.GenerateAccessToken(
            key: _bearerSettings.Key,
            issuer: _bearerSettings.Issuer,
            audience: _bearerSettings.Audience,
            expires: DateTime.UtcNow.AddSeconds(_bearerSettings.AccessTokenLifetimeInSeconds),
            claims: claims);

        return accessToken;
    }

    private static string BuildCallback(
        string callback,
        string accessToken,
        DateTime accessTokenExpiryTime,
        string refreshToken,
        DateTime refreshTokenExpiryTime)
    {
        if (string.IsNullOrEmpty(callback)) throw new ArgumentException();

        callback = callback.Split("?")[0];

        var parameters = new Dictionary<string, string>
        {
            { "access_token", accessToken },
            { "access_token_expires_at", accessTokenExpiryTime.ToString("dd.MM.yyyy HH:mm:ss") },
            { "refresh_token", refreshToken },
            { "refresh_token_expires_at", refreshTokenExpiryTime.ToString("dd.MM.yyyy HH:mm:ss") }
        };

        var urlParameters = string.Join("&", parameters
            .Where(kvp => !string.IsNullOrEmpty(kvp.Value))
            .Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));

        return string.Join("?", callback, urlParameters);
    }
}