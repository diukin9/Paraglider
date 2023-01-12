using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.Application.Features.Account.Commands;
using Paraglider.Application.Features.Auth.Commands;
using Paraglider.Application.Features.Users.Queries;
using Paraglider.Infrastructure.Common.Enums;
using Paraglider.Infrastructure.Common.Response;
using static Paraglider.Infrastructure.Common.AppData;
using ActionResult = Paraglider.Infrastructure.Common.Helpers.ActionResult;

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
    public async Task<IActionResult> CookieAuth(
        [FromBody] CookieAuthRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return ActionResult.Create(response);
    }

    [HttpPost]
    [Authorize]
    [Route("web/logout")]
    public async Task<IActionResult> CookieLogout(
        [FromQuery] CookieLogoutRequest request, 
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return ActionResult.Create(response);
    }

    [HttpPost("mobile/token")]
    public async Task<IActionResult> GetToken(
        [FromBody] GetTokenRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return ActionResult.Create(response);
    }

    [HttpPut("mobile/token")]
    public async Task<IActionResult> UpdateToken(
        [FromBody] RefreshTokenRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return ActionResult.Create(response);
    }

    [HttpGet("mobile/{scheme}")]
    public async Task<IActionResult> MobileExtAuth([FromRoute] string scheme, CancellationToken token)
    {
        var request = new ConfigureAuthPropertiesRequest(scheme, AuthScheme.Token);
        var response = await _mediator.Send(request, token);
        if (!response.IsOk) return ActionResult.Create(response);
        var properties = response.Metadata!.DataObject!;
        return Challenge(properties, scheme);
    }

    [HttpGet("web/{scheme}")]
    public async Task<IActionResult> WebExtAuth(
        [FromRoute] string scheme,
        [FromQuery] string callback,
        CancellationToken cancellationToken)
    {
        var request = new ConfigureAuthPropertiesRequest(scheme, AuthScheme.Cookie, callback);
        var response = await _mediator.Send(request, cancellationToken);
        if (!response.IsOk) return ActionResult.Create(response);
        var properties = response.Metadata!.DataObject!;
        return Challenge(properties, scheme);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route($"/{ExternalAuthHandlerRelativePath}")]
    public async Task<IActionResult> ExternalAuthHandler(
        [FromQuery] string remoteError, 
        [FromQuery] string callback,
        [FromQuery] AuthScheme scheme,
        CancellationToken cancellationToken)
    {
        if (!string.IsNullOrEmpty(remoteError))
        {
            return BadRequest(new ErrorResponse() { Message = remoteError });
        }

        if (scheme == AuthScheme.Token) callback = MobileUrl;

        var userExistRequest = new CheckUserExistenceByExternalLoginInfoRequest();
        var userExistResponse = await _mediator.Send(userExistRequest, cancellationToken);
        if (!userExistResponse.IsOk) return ActionResult.Create(userExistResponse);

        if (!userExistResponse.Content)
        {
            var createUserRequest = new CreateExternalUserRequest();
            var createUserResponse = await _mediator.Send(createUserRequest, cancellationToken);
            if (!createUserResponse.IsOk) return ActionResult.Create(createUserResponse);
        }

        var request = new ExternalAuthRequest() { Callback = callback, Scheme = scheme };
        var response = await _mediator.Send(request, cancellationToken);
        if (!response.IsOk) return ActionResult.Create(response);

        return Redirect(response.Content!);
    }
}