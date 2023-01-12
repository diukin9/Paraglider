using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Application.Controllers;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using Paraglider.MailService;
using Paraglider.MailService.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Paraglider.Application.Features.Account.Commands;

public class SendPasswordResetMailRequest : IRequest<InternalOperation>
{
    [Required, EmailAddress]
    [JsonPropertyName("email")]
    public string Email { get; set; } = null!;
}

public class SendPasswordResetMailCommandHandler 
    : IRequestHandler<SendPasswordResetMailRequest, InternalOperation>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMailService _mailService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LinkGenerator _linkGenerator;

    public SendPasswordResetMailCommandHandler(
        UserManager<ApplicationUser> userManager,
        IMailService mailService,
        IHttpContextAccessor httpContextAccessor,
        LinkGenerator linkGenerator)
    {
        _userManager = userManager;
        _mailService = mailService;
        _httpContextAccessor = httpContextAccessor;
        _linkGenerator = linkGenerator;
    }

    public async Task<InternalOperation> Handle(
        SendPasswordResetMailRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new InternalOperation();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) return operation.AddError("Пользователь не найден");

        if (!await _userManager.IsEmailConfirmedAsync(user))
        {
            return operation.AddError("Почтовый ящик не подтвержден");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
         
        var handlerUrl = _linkGenerator.GetUriByAction(
            httpContext: _httpContextAccessor.HttpContext!,
            action: "reset-password",
            controller: nameof(AccountController).Replace("Controller", string.Empty).ToLower(),
            values: new { userId = user.Id, token });

        await _mailService.SendAsync(
            MailMessage.ResetPassword(request.Email, handlerUrl!), 
            cancellationToken);

        return operation.AddSuccess();
    }
}