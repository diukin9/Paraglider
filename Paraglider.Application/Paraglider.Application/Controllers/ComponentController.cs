using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.Application.Features.Components.Commands;
using Paraglider.Application.Features.Components.Queries;
using Paraglider.Application.Features.Planning.Commands;
using ActionResult = Paraglider.Infrastructure.Common.Helpers.ActionResult;

namespace Paraglider.Application.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}")]
public class ComponentController : ControllerBase
{
    private readonly IMediator _mediator;

    public ComponentController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("components")]
    public async Task<IActionResult> Get(
        [FromQuery] GetComponentsRequest request, 
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return ActionResult.Create(response);
    }

    [HttpGet("components/{id}")]
    public async Task<IActionResult> GetById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetComponentByIdRequest(id), cancellationToken);
        return ActionResult.Create(response);
    }

    [HttpPost("user/planning/components")]
    public async Task<IActionResult> AddComponent(
        [FromBody] AddComponentToPlanningRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return ActionResult.Create(response);
    }

    [HttpDelete("user/planning/components")]
    public async Task<IActionResult> RemoveComponent(
        [FromBody] RemoveComponentFromPlanningRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return ActionResult.Create(response);
    }

    [HttpPost("user/favourites")]
    public async Task<IActionResult> AddToFavourites(
        [FromBody] AddComponentToFavouritesRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return ActionResult.Create(response);
    }

    [HttpDelete("user/favourites")]
    public async Task<IActionResult> RemoveFromFavourites(
        [FromBody] RemoveComponentFromFavouritesRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return ActionResult.Create(response);
    }
}
