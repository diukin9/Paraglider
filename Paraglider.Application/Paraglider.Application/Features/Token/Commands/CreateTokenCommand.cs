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
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Application.Features.Token.Commands;

public class CreateTokenRequest : IRequest<OperationResult<Tokens>>
{
    [Required] 
    public string Login { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}

public class CreateTokenCommandHandler 
    : IRequestHandler<CreateTokenRequest, OperationResult<Tokens>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly BearerSettings _bearerSettings;

    public CreateTokenCommandHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        BearerSettings bearerSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _bearerSettings = bearerSettings;
    }

    public async Task<OperationResult<Tokens>> Handle(
        CreateTokenRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult<Tokens>();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //получаем пользователя
        var user = await _userManager.FindByNameIdentifierAsync(request.Login);
        if (user is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(ApplicationUser)));
        }

        var checkPasswordResult = await _signInManager
            .CheckPasswordSignInAsync(user, request.Password, false);

        //проверяем, что передан верный пароль
        if (!checkPasswordResult.Succeeded)
        {
            return operation.AddError("Wrong password");
        }

        //прокинем необходимые клеймы для будушей генерации токена
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, request.Login),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        //сгенерируем access_token
        var accessToken = TokenHelper.GenerateAccessToken(
            key: _bearerSettings.Key,
            issuer: _bearerSettings.Issuer,
            audience: _bearerSettings.Audience,
            expires: DateTime.UtcNow.AddSeconds(_bearerSettings.AccessTokenLifetimeInSeconds),
            claims: claims);

        //сгенерируем refresh_token
        var refreshToken = TokenHelper.GenerateRefreshToken();
        var expiryTime = DateTime.UtcNow.AddSeconds(_bearerSettings.RefreshTokenLifetimeInSeconds);

        //сохраним refresh_token внутри пользователя
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = expiryTime;
        await _userManager.UpdateAsync(user);

        var token = new Infrastructure.Common.Models.Tokens()
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Expiration = user.RefreshTokenExpiryTime,
        };

        return operation.AddSuccess(string.Empty, token);
    }
}