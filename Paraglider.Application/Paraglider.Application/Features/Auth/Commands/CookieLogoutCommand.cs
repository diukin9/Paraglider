using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Response;

namespace Paraglider.Application.Features.Auth.Commands;

public class CookieLogoutRequest : IRequest<OperationResult>
{

}

public class CookieLogoutCommandHandler : IRequestHandler<CookieLogoutRequest, OperationResult>
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public CookieLogoutCommandHandler(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<OperationResult> Handle(
        CookieLogoutRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        await _signInManager.SignOutAsync();
        return operation.AddSuccess("Пользователь успешно вышел из аккаунта");
    }
}
