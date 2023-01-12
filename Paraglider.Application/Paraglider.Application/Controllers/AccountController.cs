using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.Application.Features.Account.Commands;
using Paraglider.Infrastructure.Common;
using ActionResult = Paraglider.Infrastructure.Common.Helpers.ActionResult;

namespace Paraglider.Application.Controllers;

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
    public async Task<IActionResult> Register(
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var registerResponse = await _mediator.Send(request, cancellationToken);
        if (!registerResponse.IsOk) return ActionResult.Create(registerResponse);

        var sendEmailResponse = await _mediator.Send(
            new SendConfirmationEmailRequest() { Email = request.Email },
            cancellationToken);

        return ActionResult.Create(sendEmailResponse);
    }

    [HttpGet]
    [AllowAnonymous]
    [Route("/[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public async Task<IActionResult> ConfirmEmail(
        [FromQuery] ConfirmEmailRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        if (!response.IsOk) return ActionResult.Create(response);
        return Redirect(AppData.RedirectOnSuccessfulMailConfirmation);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("reset-password")]
    public async Task<IActionResult> ResetPassword(
        [FromBody] SendPasswordResetMailRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return ActionResult.Create(response);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("reset-password-handler")]
    public async Task<IActionResult> ResetPasswordHandler(
        [FromBody] SetNewPasswordRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return ActionResult.Create(response);
    }
}
