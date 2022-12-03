using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;

namespace Paraglider.API.Features.Users.Commands;

public record SetNewPasswordCommand(Guid UserId,
    string Token,
    string NewPassword,
    string ConfirmPassword) : IRequest<OperationResult>;

public class ResetPasswordCommandValidator : AbstractValidator<SetNewPasswordCommand>
{
    public ResetPasswordCommandValidator()
    {
        RuleSet(AppData.DefaultRuleSetName, () =>
        {
            RuleFor(e => e.NewPassword)
                .NotEmpty()
                .MinimumLength(8);

            RuleFor(e => e.ConfirmPassword)
                .NotEmpty()
                .Equal(e => e.NewPassword);
        });
    }
}

public class ResetPasswordCommandHandler : IRequestHandler<SetNewPasswordCommand, OperationResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<SetNewPasswordCommand> _validator;

    public ResetPasswordCommandHandler(UserManager<ApplicationUser> userManager,
        IUserRepository userRepository,
        IValidator<SetNewPasswordCommand> validator)
    {
        _userManager = userManager;
        _userRepository = userRepository;
        _validator = validator;
    }

    public async Task<OperationResult> Handle(SetNewPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindByIdAsync(request.UserId);
        if (user == null)
            throw new ArgumentException($"User with id={request.UserId} not exist");

        var resetPasswordResult = await _userManager
            .ResetPasswordAsync(user, request.Token, request.NewPassword);

        if (!resetPasswordResult.Succeeded)
            return OperationResult.Error(string.Join(';', resetPasswordResult.Errors));

        return OperationResult.Success(AppData.Messages.PasswordSuccesfullyChanged);
    }
}