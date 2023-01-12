using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Response;
using Paraglider.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using Paraglider.Infrastructure.Common.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Paraglider.Infrastructure.Common.Helpers;

namespace Paraglider.Application.Features.Auth.Commands;

public class CookieAuthRequest : IRequest<InternalOperation>
{
    [Required]
    public string Login { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}

public class CookieAuthCommandHandler : IRequestHandler<CookieAuthRequest, InternalOperation>
{
    private readonly IHttpContextAccessor _accessor;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public CookieAuthCommandHandler(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor accessor)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _accessor = accessor;
    }

    public async Task<InternalOperation> Handle(
        CookieAuthRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new InternalOperation();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        //получаем пользователя
        var user = await _userManager.FindByEmailAsync(request.Login)
                ?? await _userManager.FindByNameAsync(request.Login);

        if (user is null)
        {
            return operation.AddError("Неверный логин или пароль");
        }

        //пытаемся авторизовать
        var signInResult = await _signInManager
            .CheckPasswordSignInAsync(user, request.Password, false);

        if (!signInResult.Succeeded) return operation.AddError("Неверный логин или пароль");

        await _accessor.HttpContext!.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            ClaimsPrincipalHelper.CreateByUser<ApplicationUser, Guid>(user),
            new AuthenticationProperties
            {
                IsPersistent = true,
                AllowRefresh = true,
                ExpiresUtc = DateTime.UtcNow.AddDays(14)
            });

        return operation.AddSuccess();
    }
}
