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
    public async Task<IActionResult> Get([FromQuery] GetComponentsRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById([FromRoute] string id)
    {
        var response = await _mediator.Send(new GetComponentByIdRequest(id), HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }
}
