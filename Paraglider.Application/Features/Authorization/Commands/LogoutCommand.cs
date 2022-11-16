using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Domain.Entities;
using Paraglider.Infrastructure;
using Paraglider.Infrastructure.Extensions;
using Reinforced.Typings.Attributes;
using static Paraglider.Infrastructure.AppData;

namespace Paraglider.API.Commands;

[TsClass]
public class LogoutRequest : IRequest<OperationResult>
{

}

public class LogoutCommandHandler : IRequestHandler<LogoutRequest, OperationResult>
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LogoutCommandHandler(
        SignInManager<ApplicationUser> signInManager,
        IHttpContextAccessor httpContextAccessor)
    {
        _signInManager = signInManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<OperationResult> Handle(LogoutRequest request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        var username = _httpContextAccessor.HttpContext!.User.Identity!.Name;
        await _signInManager.SignOutAsync();

        operation.AddSuccess(Messages.LogOut_SuccessfulLogOut(username!));
        return operation;
    }
}
