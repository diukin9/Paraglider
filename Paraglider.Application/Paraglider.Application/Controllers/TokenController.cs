using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.Application.Features.Account.Commands;
using Paraglider.Application.Features.Token.Commands;
using Paraglider.Application.Features.Users.Queries;
using static Paraglider.Infrastructure.Common.AppData;

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

    [HttpPost]
    public async Task<IActionResult> CreateToken([FromBody] CreateTokenRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response.GetDataObject()) : BadRequest(response);
    }

    [HttpPut]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response.GetDataObject()) : BadRequest(response);
    }

    [HttpDelete]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> RevokeToken([FromBody] RevokeTokenRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response.GetDataObject()) : BadRequest(response);
    }

    [HttpGet("{scheme}")]
    public async Task<IActionResult> GoToProviderLoginPage(
        [FromRoute] string scheme, 
        [FromQuery] string callback,
        CancellationToken cancellationToken)
    {
        var request = new ConfigureAuthPropertiesRequest(scheme, callback);
        var response = await _mediator.Send(request, cancellationToken);

        if (!response.IsOk) return BadRequest(response);
        var properties = response.Metadata!.DataObject!;

        return Challenge(properties, scheme);
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    [Route($"/{ExternalAuthHandlerRelativePath}")]
    public async Task<IActionResult> HandleAndTransferToken(
        [FromQuery] string remoteError, 
        [FromQuery] string callback,
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

        var request = new GetUrlForTokenTransferRequest() { Callback = callback };
        var response = await _mediator.Send(request, cancellationToken);
        if (!response.IsOk) return BadRequest(response);

        return Redirect(response.GetDataObject()!);
    }
}