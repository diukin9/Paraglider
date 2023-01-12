using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Application.Extensions;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using System.Security.Claims;

namespace Paraglider.Application.Features.Users.Queries;

public class CheckUserExistenceByExternalLoginInfoRequest : IRequest<InternalOperation<bool>>
{

}

public class CheckUserExistenceByExternalLoginInfoQueryHandler 
    : IRequestHandler<CheckUserExistenceByExternalLoginInfoRequest, InternalOperation<bool>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public CheckUserExistenceByExternalLoginInfoQueryHandler(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<InternalOperation<bool>> Handle(
        CheckUserExistenceByExternalLoginInfoRequest request, 
        CancellationToken cancellationToken)
    {
        var operation = new InternalOperation<bool>();

        //получаем ExternalLoginInfo
        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info is null) operation.AddError("Не удалось получить данные пользователя");

        //получаем провайдера и внешний id
        var provider = Enum.Parse<AuthProvider>(info!.LoginProvider);
        var externalId = info.Principal.Claims.GetByClaimType(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(externalId))
        {
            return operation.AddError("Не удалось получить данные пользователя");
        }

        //получаем пользователя
        var user = await _userManager.FindByExternalLoginInfoAsync(provider, externalId);
        return operation.AddSuccess(user is not null);
    }
}
