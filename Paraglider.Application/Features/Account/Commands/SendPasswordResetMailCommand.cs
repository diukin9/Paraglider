using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.API.Controllers;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.MailService;
using Paraglider.MailService.Models;
using System.ComponentModel.DataAnnotations;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Account.Commands;

public class SendPasswordResetMailRequest : IRequest<OperationResult>
{
    [Required, EmailAddress] public string Email { get; set; } = null!;
}

public class SendPasswordResetMailCommandHandler 
    : IRequestHandler<SendPasswordResetMailRequest, OperationResult>
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

    public async Task<OperationResult> Handle(
        SendPasswordResetMailRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(user)));

        if (!await _userManager.IsEmailConfirmedAsync(user))
            return operation.AddError("Сначала необходимо подтвердить почтовый ящик.");

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
         
        var handlerUrl = _linkGenerator.GetUriByAction(
            httpContext: _httpContextAccessor.HttpContext!,
            action: "reset-password",
            controller: nameof(AccountController).Replace("Controller", string.Empty).ToLower(),
            values: new { userId = user.Id, token });

        await _mailService.SendAsync(MailMessage.ResetPassword(request.Email, handlerUrl!), cancellationToken);

        return operation.AddSuccess($"Инструкция для смены пароля была отправлена на почту '{request.Email}'");
    }
}