using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.API.Features.Account.Commands;
using Paraglider.Infrastructure.Common;

namespace Paraglider.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/account")]
public class AccountController : ControllerBase
{
    private readonly IMediator _mediator;

    public AccountController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserRequest request,
    CancellationToken cancellationToken)
    {
        var registerResponse = await _mediator.Send(request, cancellationToken);
        if (!registerResponse.IsOk) return BadRequest(registerResponse);

        var sendConfirmMailResponse = await _mediator.Send(
            new SendConfirmationEmailRequest(request.Email),
            cancellationToken);

        if (!sendConfirmMailResponse.IsOk) return BadRequest(sendConfirmMailResponse);

        var message = $"{registerResponse.Metadata!.Message} {sendConfirmMailResponse.Metadata!.Message}";
        return Ok(new OperationResult().AddSuccess(message));
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("/[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ConfirmEmail(
        [FromQuery] ConfirmEmailRequest confirmEmailCommand,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(confirmEmailCommand, cancellationToken);
        if (!response.IsOk) return BadRequest(response);
        return Redirect(AppData.RedirectOnSuccessfulMailConfirmation);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("reset-password")]
    public async Task<IActionResult> ResetPassword(
        [FromBody] SendPasswordResetMailRequest command,
        CancellationToken cancellationToken)
    {
        var passwordResetMailResult = await _mediator.Send(command, cancellationToken);
        if (!passwordResetMailResult.IsOk)
            return BadRequest(passwordResetMailResult);

        return Ok(passwordResetMailResult);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("reset-password-handler")]
    public async Task<IActionResult> ResetPasswordHandler(
        [FromBody] SetNewPasswordRequest setNewPasswordCommand,
        CancellationToken cancellationToken)
    {
        var resetPasswordResult = await _mediator.Send(setNewPasswordCommand, cancellationToken);
        if (!resetPasswordResult.IsOk) return BadRequest(resetPasswordResult);
        return Ok(resetPasswordResult);
    }
}
