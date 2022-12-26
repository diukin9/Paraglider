using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Application.Extensions;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using System.Security.Claims;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Application.Features.Token.Commands;

public record ExternalAuthRequest() : IRequest<OperationResult>;

public class ExternalAuthCommandHandler 
    : IRequestHandler<ExternalAuthRequest, OperationResult>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public ExternalAuthCommandHandler(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }

    public async Task<OperationResult> Handle(
        ExternalAuthRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //получаем ExternalLoginInfo
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(ExternalLoginInfo)));
        }

        //получаем провайдера и внешний id
        var provider = Enum.Parse<AuthProvider>(info.LoginProvider);
        var externalId = info.Principal.Claims.GetByClaimType(ClaimTypes.NameIdentifier);

        //получаем пользователя на основе ExternalLoginInfo
        var user = await _userManager.FindByExternalLoginInfoAsync(provider, externalId!);
        if (user is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(ApplicationUser)));
        }

        //если у пользователя нет такого UserLoginInfo - создаем
        if (await _userManager.FindUserLoginInfoAsync(user, info.LoginProvider, info.ProviderKey) is null)
        { 
            var identityResult = await _userManager.AddLoginAsync(user, info);
            if (!identityResult.Succeeded)
            {
                return operation.AddError(string.Join("; ", identityResult.Errors));
            }
        }

        //авторизуем пользователя
        var signInResult = await _signInManager.ExternalLoginSignInAsync(
            info.LoginProvider,
            info.ProviderKey,
            true);

        if (!signInResult.Succeeded)
        {
            return operation.AddError("Ошибка при попытке внешней авторизации.");
        }

        return operation.AddSuccess("Пользователь успешно авторизан через внешнего провайдера.");
    }
}