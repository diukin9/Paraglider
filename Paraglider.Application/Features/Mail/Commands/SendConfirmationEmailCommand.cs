using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.MailService;
using Paraglider.MailService.Models;

namespace Paraglider.API.Features.Mail.Commands;

public record SendConfirmationEmailCommand(ApplicationUser User) : IRequest<OperationResult>;

public class SendConfirmationEmailCommandHandler : IRequestHandler<SendConfirmationEmailCommand, OperationResult>
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

    public async Task<OperationResult> Handle(SendConfirmationEmailCommand request, CancellationToken cancellationToken)
    {
        var user = request.User;

        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

        var confirmationLink = linkGenerator.GetUriByAction(
            httpContextAccessor.HttpContext!,
            "ConfirmEmail",
            "Authorization",
            new {userId = user.Id, token});
        
        

        if (confirmationLink == null)
            throw new Exception("Не удалось сгенерировать ссылку для подтверждения email");
        if (user.Email == null)
            throw new ArgumentNullException(nameof(user.Email));

        var mailMessage = MailMessage.MailConfirmation(user.Email, confirmationLink);

        await mailService.SendAsync(mailMessage, cancellationToken);


        return OperationResult.Success(AppData.Messages.ConfirmationEmailSent(user.Email));
    }
}