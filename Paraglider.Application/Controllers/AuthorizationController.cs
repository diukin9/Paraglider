using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.API.Features.Authorization.Commands;
using Paraglider.API.Features.Mail.Commands;
using Paraglider.API.Features.Registration.Commands;
using Paraglider.API.Features.Users.Commands;
using Paraglider.API.Features.Users.Queries;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;

namespace Paraglider.API.Controllers;

public class AuthorizationController : Controller
{
    private readonly IMediator _mediator;

    public AuthorizationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("/api/basic-auth")]
    public async Task<IActionResult> BasicAuthorization([FromBody] BasicAuthRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    [Authorize]
    [Route("/api/logout")]
    public async Task<IActionResult> Logout()
    {
        var response = await _mediator.Send(new LogoutRequest(), HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("/external-auth")]
    public async Task<IActionResult> VerifyUserAuthentication(
        [FromQuery] string provider, 
        [FromQuery] string returnUrl)
    {
        var response = await _mediator.Send(
            new ConfigureExternalAuthPropertiesRequest(provider, returnUrl),
            HttpContext.RequestAborted);

        if (!response.IsOk) return BadRequest(response);
        var properties = (AuthenticationProperties)response.Metadata!.DataObject!;
        return Challenge(properties, provider);
    }

    [AllowAnonymous]
    [Route("/external-auth-handler")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ExternalAuthorization(
        [FromQuery] string remoteError, 
        [FromQuery] string returnUrl)
    {
        if (!string.IsNullOrEmpty(remoteError))
        {
            var operation = new OperationResult().AddError(remoteError);
            return BadRequest(operation);
        }
      
        var infoResponse = await _mediator.Send(
            new GetUserByExternalLoginInfoRequest(),
            HttpContext.RequestAborted);

        if (!infoResponse.IsOk) return BadRequest(infoResponse);
        var info = infoResponse.GetDataObject()!;

        var userResponse = await _mediator.Send(
            new GetUserByExternalLoginInfoRequest(),
            HttpContext.RequestAborted);

        if (!userResponse.IsOk)
        {
            var createResponse = await _mediator.Send(
                new CreateExternalUserRequest(),
                HttpContext.RequestAborted);

            if (!createResponse.IsOk) return BadRequest(createResponse);
        }

        var response = await _mediator.Send(
            new ExternalAuthRequest(),
            HttpContext.RequestAborted);

        if (!response.IsOk) return BadRequest(response);

        return Redirect(returnUrl);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("api/register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserCommand command,
        CancellationToken cancellationToken)
    {
        var registerResponse = await _mediator.Send(command, cancellationToken);

        if (!registerResponse.IsOk) return BadRequest(registerResponse);

        var user = (ApplicationUser) registerResponse.Metadata!.DataObject!;

        var sendConfirmMailResponse = await _mediator.Send(new SendConfirmationEmailCommand(user),
            cancellationToken);

        if (!sendConfirmMailResponse.IsOk) return BadRequest(sendConfirmMailResponse.Metadata!.Message);

        return Ok(registerResponse.Metadata.Message);
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("confirm-email")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailCommand confirmEmailCommand,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(confirmEmailCommand, cancellationToken);

        if (!response.IsOk) return BadRequest(response);

        return Redirect((string) response.Metadata!.DataObject!);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("api/reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] SendPasswordResetMailCommand command,
        CancellationToken cancellationToken)
    {
        var passwordResetMailResult = await _mediator.Send(command, cancellationToken);

        if (!passwordResetMailResult.IsOk)
            return BadRequest(passwordResetMailResult.Metadata!.Message);

        return Ok(passwordResetMailResult.Metadata!.Message);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("api/set-new-password")]
    public async Task<IActionResult> ResetPasswordHandler([FromBody] SetNewPasswordCommand setNewPasswordCommand,
        CancellationToken cancellationToken)
    {
        var resetPasswordResult = await _mediator.Send(setNewPasswordCommand, cancellationToken);

        if (!resetPasswordResult.IsOk) return BadRequest(resetPasswordResult.Metadata!.Message);

        return Ok(resetPasswordResult.Metadata!.Message);
    }
}
