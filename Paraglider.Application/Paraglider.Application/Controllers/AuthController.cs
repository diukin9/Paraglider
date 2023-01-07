using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.Application.Features.Account.Commands;
using Paraglider.Application.Features.Auth.Commands;
using Paraglider.Application.Features.Users.Queries;
using Paraglider.Infrastructure.Common.Enums;
using System.Net;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Application.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController : Controller
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("cookie/sign-in")]
    public async Task<IActionResult> CookieAuth([FromBody] CookieAuthRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    [Authorize]
    [Route("cookie/logout")]
    public async Task<IActionResult> CookieLogout()
    {
        var response = await _mediator.Send(new CookieLogoutRequest(), HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpPost("token")]
    public async Task<IActionResult> GetToken([FromBody] GetTokenRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response.GetDataObject()) : BadRequest(response);
    }

    [HttpPut("token")]
    public async Task<IActionResult> UpdateToken([FromBody] RefreshTokenRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response.GetDataObject()) : BadRequest(response);
    }

    [HttpGet("common/{scheme}")]
    public async Task<IActionResult> GoToProviderLoginPage(
        [FromRoute] string scheme, 
        [FromQuery] string callback,
        [FromQuery] AuthType authType,
        CancellationToken cancellationToken)
    {
        var request = new ConfigureAuthPropertiesRequest(scheme, callback, authType);
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

    [HttpGet("mobile-auth/{scheme}")]
    public async Task Get([FromRoute] string scheme)
    {
        var auth = await Request.HttpContext.AuthenticateAsync(scheme);

        if (!auth.Succeeded
            || auth?.Principal == null
            || !auth.Principal.Identities.Any(id => id.IsAuthenticated))
        {
            // Not authenticated, challenge
            await Request.HttpContext.ChallengeAsync(scheme);
        }
        else
        {
            var claims = auth.Principal.Identities.FirstOrDefault()?.Claims;
            var email = string.Empty;
            email = claims?.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;

            // Get parameters to send back to the callback
            var qs = new Dictionary<string, string>
                {
                    { "access_token", auth.Properties.GetTokenValue("access_token") },
                    { "refresh_token", auth.Properties.GetTokenValue("refresh_token") ?? string.Empty },
                    { "expires_in", (auth.Properties.ExpiresUtc?.ToUnixTimeSeconds() ?? -1).ToString() },
                    { "email", email }
                };

            // Build the result url
            var url = "paragliderapp" + "://#" + string.Join(
                "&",
                qs.Where(kvp => !string.IsNullOrEmpty(kvp.Value) && kvp.Value != "-1")
                .Select(kvp => $"{WebUtility.UrlEncode(kvp.Key)}={WebUtility.UrlEncode(kvp.Value)}"));

            // Redirect to final url
            Request.HttpContext.Response.Redirect(url);
        }
    }
}