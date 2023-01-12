using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Paraglider.Application.Features.Account.Commands;

public class ConfirmEmailRequest : IRequest<InternalOperation<string>>
{
    [Required]
    [JsonPropertyName("user_id")]
    public Guid UserId { get; set; }

    [Required]
    [JsonPropertyName("token")]
    public string Token { get; set; } = null!;
}

public class ConfirmEmailCommandHandler
    : IRequestHandler<ConfirmEmailRequest, InternalOperation<string>>
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

    public async Task<InternalOperation<string>> Handle(
        ConfirmEmailRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new InternalOperation<string>();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        var user = await userRepository.FindByIdAsync(request.UserId);
        if (user == null) return operation.AddError("Пользователь не найден");

        var confirmationResult = await userManager.ConfirmEmailAsync(user, request.Token);

        if (!confirmationResult.Succeeded)
        {
            return operation.AddError(string.Join(';', confirmationResult.Errors));
        }

        return operation.AddSuccess();
    }
}