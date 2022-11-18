using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Domain.Entities;
using Paraglider.Infrastructure;
using Paraglider.Infrastructure.Extensions;
using Reinforced.Typings.Attributes;
using static Paraglider.Infrastructure.AppData;

namespace Paraglider.API.Features.Authorization.Commands;

[TsClass]
public class LogoutRequest : IRequest<OperationResult>
{

}

public class LogoutCommandHandler : IRequestHandler<LogoutRequest, OperationResult>
{
    private readonly SignInManager<ApplicationUser> _signInManager;

    public LogoutCommandHandler(SignInManager<ApplicationUser> signInManager)
    {
        _signInManager = signInManager;
    }

    public async Task<OperationResult> Handle(LogoutRequest request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();
        await _signInManager.SignOutAsync();
        operation.AddSuccess(Messages.SuccessfulLogout);
        return operation;
    }
}
