using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.Application.Features.Users.Commands;
using Paraglider.Application.Features.Users.Queries;
using ActionResult = Paraglider.Infrastructure.Common.Helpers.ActionResult;

namespace Paraglider.Application.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/user")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetCurrentUser(
        [FromQuery] GetCurrentUserRequest request, 
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return ActionResult.Create(response);
    }

    [HttpPatch("city")]
    public async Task<IActionResult> ChangeCity(
        [FromBody] ChangeUserCityRequest request, 
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return ActionResult.Create(response);
    }
}

