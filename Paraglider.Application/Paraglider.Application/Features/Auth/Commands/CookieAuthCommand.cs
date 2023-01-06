using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Response;
using Paraglider.Infrastructure.Common;
using System.ComponentModel.DataAnnotations;
using Paraglider.Infrastructure.Common.Extensions;

namespace Paraglider.Application.Features.Auth.Commands;

public class CookieAuthRequest : IRequest<OperationResult>
{
    [Required]
    public string Login { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}

public class CookieAuthCommandHandler : IRequestHandler<CookieAuthRequest, OperationResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public CookieAuthCommandHandler(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<OperationResult> Handle(
        CookieAuthRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

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
            .PasswordSignInAsync(user, request.Password, true, false);

        if (!signInResult.Succeeded)
        {
            return operation.AddError("Неверный логин или пароль");
        }

        return operation.AddSuccess("Пользователь успешно авторизован");
    }
}
