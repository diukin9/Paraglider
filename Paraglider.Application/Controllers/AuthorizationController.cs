using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.API.Features.Authorization.Commands;
using Paraglider.API.Features.Users.Commands;
using Paraglider.API.Features.Users.Queries;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;

namespace Paraglider.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("/api/v{version:apiVersion}")]
public class AuthorizationController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthorizationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("basic-auth")]
    public async Task<IActionResult> BasicAuthorization([FromBody] BasicAuthRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [AllowAnonymous]
    [HttpGet("/external-auth")]
    public async Task<IActionResult> VerifyUserAuthentication(
        [FromQuery] string provider,
        [FromQuery] string returnUrl)
    {
        var request = new ConfigureExternalAuthPropertiesRequest(provider, returnUrl);
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        if (!response.IsOk) return BadRequest(response);
        var properties = (AuthenticationProperties)response.GetDataObject()!;
        return Challenge(properties, request.Provider);
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

        //ищем пользователя по ExternalLoginInfo
        var response = await _mediator.Send(
            new GetUserByExternalLoginInfoRequest(),
            HttpContext.RequestAborted);

        //если не существует - создаем
        if (!response.IsOk)
        {
            response = await _mediator.Send(
                new CreateExternalUserRequest(),
                HttpContext.RequestAborted);

            if (!response.IsOk) return BadRequest(response);
        }

        //авторизуем пользователя
        response = await _mediator.Send(
            new ExternalAuthRequest(),
            HttpContext.RequestAborted);

        if (!response.IsOk) return BadRequest(response);

        return Redirect(returnUrl);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        var response = await _mediator.Send(new LogoutRequest(), HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }
}
