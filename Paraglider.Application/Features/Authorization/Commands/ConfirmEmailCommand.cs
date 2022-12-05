using System.Net;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;

namespace Paraglider.API.Features.Authorization.Commands;

public record ConfirmEmailCommand : IRequest<OperationResult<string>>
{
    public Guid UserId { get; init; }
    public string Token { get; init; } = null!;
}

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, OperationResult<string>>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IUserRepository userRepository;

    public ConfirmEmailCommandHandler(UserManager<ApplicationUser> userManager,
        IUserRepository userRepository)
    {
        this.userManager = userManager;
        this.userRepository = userRepository;
    }

    public async Task<OperationResult<string>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult<string>();

        var user = await userRepository.FindByIdAsync(request.UserId);

        if (user == null)
            return operation.AddError(AppData.ExceptionMessages.ObjectNotFound(nameof(user)));

        var confirmationResult = await userManager.ConfirmEmailAsync(user, request.Token);

        if (!confirmationResult.Succeeded)
            return operation.AddError(string.Join(';', confirmationResult.Errors));

        var redirectUrl = WebUtility.UrlEncode(AppData.RedirectOnSuccessfulMailConfirmation);

        return operation.AddSuccess(AppData.Messages.SuccessfulEmailConfirmation, redirectUrl);
    }
}