using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.API.Features.Components.Queries;
using Paraglider.API.Features.Planning.Commands;
using Paraglider.API.Features.Users.Commands;

namespace Paraglider.API.Controllers;

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

    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] Guid categoryId,
        [FromQuery] string? sorterKey = null, //TODO обсудить сортировку компонентов и реализовать ее
        [FromQuery] int? perPage = 15,
        [FromQuery] int? page = 1)
    {
        var response = await _mediator.Send(
            new GetComponentsRequest(categoryId, perPage!.Value, page!.Value, sorterKey), 
            HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var response = await _mediator.Send(new GetComponentByIdRequest(id), HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpPost("user/planning/components")]
    public async Task<IActionResult> AddComponent([FromBody] AddComponentToPlanningRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("user/planning/components")]
    public async Task<IActionResult> RemoveComponent([FromBody] RemoveComponentFromPlanningRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpPost("user/favourites")]
    public async Task<IActionResult> AddToFavourites([FromBody] AddComponentToFavouritesRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("user/favourites")]
    public async Task<IActionResult> RemoveFromFavourites([FromBody] RemoveComponentFromFavouritesRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }
}
