using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Application.Extensions;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Helpers;
using Paraglider.Infrastructure.Common.Models;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace Paraglider.Application.Features.Token.Commands;

public class GetUrlForTokenTransferRequest : IRequest<OperationResult<string>>
{
    [Required]
    public string Callback { get; set; } = null!;
}

public class GetUrlForTokenTransferCommandHandler 
    : IRequestHandler<GetUrlForTokenTransferRequest, OperationResult<string>>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly BearerSettings _bearerSettings;

    public GetUrlForTokenTransferCommandHandler(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager, 
        BearerSettings bearerSettings)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _bearerSettings = bearerSettings;
    }

    public async Task<OperationResult<string>> Handle(
        GetUrlForTokenTransferRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult<string>();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //получаем ExternalLoginInfo
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info is null) return operation.AddError("ExternalLoginInfo was null");

        //получаем пользователя
        var user = await GetUserByExternalLoginInfoAsync(info);
        if (user is null) return operation.AddError("User not found");

        //если у пользователя нет такого UserLoginInfo - создаем
        if (await _userManager.FindUserLoginInfoAsync(user, info.LoginProvider, info.ProviderKey) is null)
        { 
            var identityResult = await _userManager.AddLoginAsync(user, info);
            if (!identityResult.Succeeded)
            {
                return operation.AddError(string.Join("; ", identityResult.Errors));
            }
        }

        //если у пользователя не прокинут refresh_token - прокидываем
        if (user.RefreshToken is null) await AssignToUserRefreshTokenAsync(user);

        //получим access_token
        var accessToken = GetAccessToken(user);

        //преобразуем callback
        var url = BuildCallback(
            request.Callback,
            accessToken,
            user.RefreshToken!,
            user.RefreshTokenExpiryTime);
        
        return operation.AddSuccess(string.Empty, url);
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
        string refreshToken,
        DateTime expiryTime)
    {
        if (string.IsNullOrEmpty(callback)) throw new ArgumentException();

        callback = callback.Split("?")[0];

        var parameters = new Dictionary<string, string>
        {
            { "access_token", accessToken },
            { "refresh_token", refreshToken },
            { "expires_in", expiryTime.ToString("dd.MM.yyyy HH:mm:ss") }
        };

        var urlParameters = string.Join("&", parameters
            .Where(kvp => !string.IsNullOrEmpty(kvp.Value))
            .Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));

        return string.Join("?", callback, urlParameters);
    }
}