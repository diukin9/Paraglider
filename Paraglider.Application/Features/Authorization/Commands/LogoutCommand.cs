using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Authorization.Commands;

public record LogoutRequest() : IRequest<OperationResult>;

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
        return operation.AddSuccess(Messages.SuccessfulLogout);
    }
}
