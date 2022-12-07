using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Attributes;
using Paraglider.Infrastructure.Common.Extensions;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Paraglider.API.Features.Account.Commands;

public class SetNewPasswordRequest : IRequest<OperationResult>
{
    [Required, NotEmptyGuid] public Guid UserId { get; set; }
    [Required] public string Token { get; set; } = null!;
    [Required, MinLength(8)] public string Password { get; set; } = null!;
    [Required, Compare(nameof(Password))] public string ConfirmPassword { get; set; } = null!;
}

public class ResetPasswordCommandHandler : IRequestHandler<SetNewPasswordRequest, OperationResult>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserRepository _userRepository;

    public ResetPasswordCommandHandler(
        UserManager<ApplicationUser> userManager,
        IUserRepository userRepository)
    {
        _userManager = userManager;
        _userRepository = userRepository;
    }

    public async Task<OperationResult> Handle(
        SetNewPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        var user = await _userRepository.FindByIdAsync(request.UserId);
        if (user == null)
            return operation.AddError(AppData.ExceptionMessages.ObjectNotFound(nameof(ApplicationUser)));

        var token = HttpUtility.UrlDecode(request.Token);

        var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token, request.Password);

        if (!resetPasswordResult.Succeeded)
        {
            var errors = resetPasswordResult.Errors.Select(x => x.Description);
            return operation.AddError(string.Join(';', errors));
        }

        return operation.AddSuccess("Пароль успешно изменен.");
    }
}