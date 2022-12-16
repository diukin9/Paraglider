using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.Application.Features.Users.Commands;
using Paraglider.Application.Features.Users.Queries;

namespace Paraglider.Application.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("current")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var response = await _mediator.Send(new GetCurrentUserRequest(), HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpPost("add-to-favourites")]
    public async Task<IActionResult> AddToFavourites([FromBody] AddComponentToFavouritesRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("remove-from-favourites")]
    public async Task<IActionResult> RemoveFromFavourites([FromBody] RemoveComponentFromFavouritesRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpPatch("city")]
    public async Task<IActionResult> ChangeCity([FromBody] ChangeUserCityRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return response.IsOk ? Ok(response.Metadata) : BadRequest(response.Metadata);
    }
}

