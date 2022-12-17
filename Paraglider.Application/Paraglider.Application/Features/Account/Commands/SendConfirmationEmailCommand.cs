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
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Application.Features.Account.Commands;

public class SendConfirmationEmailRequest : IRequest<OperationResult>
{
    [Required, EmailAddress] public string Email { get; set; }

    public SendConfirmationEmailRequest(string email)
    {
        Email = email;
    }
}

public class SendConfirmationEmailCommandHandler
    : IRequestHandler<SendConfirmationEmailRequest, OperationResult>
{
    private readonly IMailService mailService;
    private readonly IHttpContextAccessor httpContextAccessor;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly LinkGenerator linkGenerator;

    public SendConfirmationEmailCommandHandler(IMailService mailService,
        IHttpContextAccessor httpContextAccessor,
        UserManager<ApplicationUser> userManager,
        LinkGenerator linkGenerator)
    {
        this.mailService = mailService;
        this.httpContextAccessor = httpContextAccessor;
        this.userManager = userManager;
        this.linkGenerator = linkGenerator;
    }

    public async Task<OperationResult> Handle(
        SendConfirmationEmailRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(ApplicationUser)));

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var confirmationLink = linkGenerator.GetUriByAction(
            httpContextAccessor.HttpContext!,
            nameof(AccountController.ConfirmEmail).ToLower(),
            nameof(AccountController).Replace("Controller", string.Empty).ToLower(),
            new { userId = user.Id, token });

        if (confirmationLink == null)
            return operation.AddError("Не удалось сгенерировать ссылку для подтверждения email");

        if (user.Email == null)
            return operation.AddError(ExceptionMessages.ValueNullOrEmpty(nameof(user.Email)));

        var mailMessage = MailMessage.MailConfirmation(user.Email, confirmationLink);

        await mailService.SendAsync(mailMessage, cancellationToken);

        var message = $"Инструкция для подтверждения почтового ящика отправлена по адресу: '{user.Email}'";
        return operation.AddSuccess(message);
    }
}