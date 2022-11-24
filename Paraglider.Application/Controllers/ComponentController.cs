using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.API.Features.Components.Queries;

namespace Paraglider.API.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/components")]
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
}
