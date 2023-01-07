using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.Application.Features.Account.Commands;
using Paraglider.Application.Features.Auth.Commands;
using Paraglider.Application.Features.Users.Queries;
using Paraglider.Infrastructure.Common.Enums;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Application.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Tags("Authorization")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : Controller
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("web/sign-in")]
    public async Task<IActionResult> CookieAuth([FromBody] CookieAuthRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    [Authorize]
    [Route("web/logout")]
    public async Task<IActionResult> CookieLogout()
    {
        var response = await _mediator.Send(new CookieLogoutRequest(), HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpPost("mobile/token")]
    public async Task<IActionResult> GetToken([FromBody] GetTokenRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response.GetDataObject()) : BadRequest(response);
    }

    [HttpPut("mobile/token")]
    public async Task<IActionResult> UpdateToken([FromBody] RefreshTokenRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response.GetDataObject()) : BadRequest(response);
    }

    [HttpGet("mobile/{scheme}")]
    public async Task<IActionResult> MobileExtAuth([FromRoute] string scheme, CancellationToken token)
    {
        var request = new ConfigureAuthPropertiesRequest(scheme, AuthType.Token);
        var response = await _mediator.Send(request, token);

        if (!response.IsOk) return BadRequest(response);
        var properties = response.Metadata!.DataObject!;

        return Challenge(properties, scheme);
    }

    [HttpGet("web/{scheme}")]
    public async Task<IActionResult> WebExtAuth(
        [FromRoute] string scheme,
        [FromQuery] string callback,
        CancellationToken cancellationToken)
    {
        var request = new ConfigureAuthPropertiesRequest(scheme, AuthType.Cookie, callback);
        var response = await _mediator.Send(request, cancellationToken);

        if (!response.IsOk) return BadRequest(response);
        var properties = response.Metadata!.DataObject!;

        return Challenge(properties, scheme);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route($"/{ExternalAuthHandlerRelativePath}")]
    public async Task<IActionResult> ExternalAuthHandler(
        [FromQuery] string remoteError, 
        [FromQuery] string callback,
        [FromQuery] AuthType authType,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(remoteError)) return BadRequest(remoteError);

        if (authType == AuthType.Token) callback = MobileUrl;

        var userRequest = new CheckUserExistenceByExternalLoginInfoRequest();
        var userResponse = await _mediator.Send(userRequest, cancellationToken);

        if (!userResponse.IsOk)
        {
            var createUserRequest = new CreateExternalUserRequest();
            var createResponse = await _mediator.Send(createUserRequest, cancellationToken);
            if (!createResponse.IsOk) return BadRequest(createResponse);
        }

        var request = new ExternalAuthRequest() { Callback = callback, AuthType = authType };
        var response = await _mediator.Send(request, cancellationToken);
        if (!response.IsOk) return BadRequest(response);

        return Redirect(response.GetDataObject()!);
    }
}