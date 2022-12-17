﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.Application.Features.Components.Queries;
using Paraglider.Application.Features.Planning.Commands;
using Paraglider.Application.Features.Users.Commands;

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
    public async Task<IActionResult> Get([FromQuery] GetComponentsRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpGet("components/{id}")]
    public async Task<IActionResult> GetById([FromRoute] string id)
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
