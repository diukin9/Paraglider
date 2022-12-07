using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Attributes;
using Paraglider.Infrastructure.Common.Extensions;
using System.ComponentModel.DataAnnotations;

namespace Paraglider.API.Features.Account.Commands;

public class ConfirmEmailRequest : IRequest<OperationResult<string>>
{
    [Required, NotEmptyGuid] public Guid UserId { get; set; }
    [Required] public string Token { get; set; } = null!;
}

public class ConfirmEmailCommandHandler
    : IRequestHandler<ConfirmEmailRequest, OperationResult<string>>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IUserRepository userRepository;

    public ConfirmEmailCommandHandler(
        UserManager<ApplicationUser> userManager,
        IUserRepository userRepository)
    {
        this.userManager = userManager;
        this.userRepository = userRepository;
    }

    public async Task<OperationResult<string>> Handle(
        ConfirmEmailRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult<string>();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        var user = await userRepository.FindByIdAsync(request.UserId);

        if (user == null)
            return operation.AddError(AppData.ExceptionMessages.ObjectNotFound(nameof(user)));

        var confirmationResult = await userManager.ConfirmEmailAsync(user, request.Token);

        if (!confirmationResult.Succeeded)
            return operation.AddError(string.Join(';', confirmationResult.Errors));

        return operation.AddSuccess("Email успешно подтвержден.");
    }
}