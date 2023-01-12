using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Web;

namespace Paraglider.Application.Features.Account.Commands;

public class SetNewPasswordRequest : IRequest<InternalOperation>
{
    [Required]
    [JsonPropertyName("user_id")]
    public Guid UserId { get; set; }

    [Required]
    [JsonPropertyName("token")]
    public string Token { get; set; } = null!;

    [Required, MinLength(8)]
    [JsonPropertyName("password")]
    public string Password { get; set; } = null!;

    [Required, Compare(nameof(Password))]
    [JsonPropertyName("password_confirmation")]
    public string ConfirmPassword { get; set; } = null!;
}

public class ResetPasswordCommandHandler : IRequestHandler<SetNewPasswordRequest, InternalOperation>
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

    public async Task<InternalOperation> Handle(
        SetNewPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new InternalOperation();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        var user = await _userRepository.FindByIdAsync(request.UserId);
        if (user == null) return operation.AddError("Пользователь не найден");

        var token = HttpUtility.UrlDecode(request.Token);

        var resetPasswordResult = await _userManager.ResetPasswordAsync(user, token, request.Password);

        if (!resetPasswordResult.Succeeded)
        {
            var errors = resetPasswordResult.Errors.Select(x => x.Description);
            return operation.AddError(string.Join(';', errors));
        }

        return operation.AddSuccess();
    }
}