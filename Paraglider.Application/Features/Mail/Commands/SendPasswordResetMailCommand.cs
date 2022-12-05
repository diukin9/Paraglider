using System.ComponentModel.DataAnnotations;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.MailService;
using Paraglider.MailService.Models;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Mail.Commands;

public record SendPasswordResetMailCommand : IRequest<OperationResult<ApplicationUser>> //TODO возвращать DTO
{
    [Required] [EmailAddress] public string Email { get; init; } = null!; //TODO совмещать виды валидации не стоит, наверное

    [Required] [Url] public string RelativePasswordResetUrl { get; init; } = null!;
}

public class SendPasswordResetMailCommandValidator : AbstractValidator<SendPasswordResetMailCommand>
{
    public SendPasswordResetMailCommandValidator()
    {
        RuleSet(DefaultRuleSetName, () =>
        {
            RuleFor(e => e.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(e => e.RelativePasswordResetUrl)
                .NotEmpty()
                .Must(e => Uri.TryCreate(e, UriKind.Relative, out _));
        });
    }
}

public class SendPasswordResetMailCommandHandler : IRequestHandler<SendPasswordResetMailCommand, OperationResult<ApplicationUser>>
{
    private readonly IValidator<SendPasswordResetMailCommand> _validator;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMailService _mailService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly LinkGenerator _linkGenerator;

    public SendPasswordResetMailCommandHandler(IValidator<SendPasswordResetMailCommand> validator,
        UserManager<ApplicationUser> userManager,
        IMailService mailService,
        IHttpContextAccessor httpContextAccessor,
        LinkGenerator linkGenerator)
    {
        _validator = validator;
        _userManager = userManager;
        _mailService = mailService;
        _httpContextAccessor = httpContextAccessor;
        _linkGenerator = linkGenerator;
    }

    public async Task<OperationResult<ApplicationUser>> Handle(SendPasswordResetMailCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult<ApplicationUser>();

        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
            return operation.AddError(string.Join(';', validateResult.Errors));

        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(user)));

        if (!await _userManager.IsEmailConfirmedAsync(user))
            return operation.AddError(ExceptionMessages.UnconfirmedEmail);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var confirmationLink = GenerateAbsolutUrl(request.RelativePasswordResetUrl, user.Id, token);

        await _mailService.SendAsync(MailMessage.ResetPassword(request.Email, confirmationLink), cancellationToken);

        return operation.AddSuccess(Messages.ConfirmationEmailSent(request.Email), user);
    }

    private string GenerateAbsolutUrl(string relativeUrl, Guid userId, string token)
    {
        var baseUrl = new Uri(_httpContextAccessor.HttpContext!.Request.GetEncodedUrl())
            .GetLeftPart(UriPartial.Authority);
        var relativeUri = new Uri(relativeUrl, UriKind.Relative);
        return $"{new Uri(new Uri(baseUrl), relativeUri).AbsoluteUri}?userId={userId}&token={token}";
    }
}