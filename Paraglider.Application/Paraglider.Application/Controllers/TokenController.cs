using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.Application.Features.Account.Commands;
using Paraglider.Application.Features.Token.Commands;
using Paraglider.Application.Features.Users.Queries;
using Paraglider.Infrastructure.Common.Response;

namespace Paraglider.Application.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/token")]
public class TokenController : Controller
{
    private readonly IMediator _mediator;

    public TokenController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("create")]
    [AllowAnonymous]
    public async Task<IActionResult> CreateToken([FromBody] CreateTokenRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response.GetDataObject()) : BadRequest(response);
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response.GetDataObject()) : BadRequest(response);
    }

    [Authorize]
    [HttpPost("revoke")]
    public async Task<IActionResult> Revoke([FromBody] RevokeTokenRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response.GetDataObject()) : BadRequest(response);
    }


    [HttpGet]
    [AllowAnonymous]
    [Route("external-auth")]
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
    [Route("external-auth-handler")]
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
