using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.API.Features.WeddingComponents.Queries;

namespace Paraglider.API.Controllers;

[Authorize]
[Route("api/components")]
public class WeddingComponentsController : Controller
{
    private readonly IMediator _mediator;

    public WeddingComponentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id) 
    {
        var response = await _mediator.Send(new GetComponentByIdRequest(id), HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }
}
