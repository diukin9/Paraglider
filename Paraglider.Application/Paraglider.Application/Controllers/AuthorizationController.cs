using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.Application.Features.Account.Commands;
using Paraglider.Application.Features.Authorization.Commands;
using Paraglider.Application.Features.Users.Queries;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Response;

namespace Paraglider.Application.Controllers;

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
        var properties = response.Metadata!.DataObject!;
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
}
