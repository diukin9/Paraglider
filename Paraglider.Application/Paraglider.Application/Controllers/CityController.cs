using MediatR;
using Microsoft.AspNetCore.Mvc;
using Paraglider.Application.Features.City.Queries;

namespace Paraglider.Application.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/cities")]
public class CityController : ControllerBase
{
    private readonly IMediator _mediator;

    public CityController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCitiesQuery(), cancellationToken);
        return response.IsOk ? Ok(response.Metadata) : BadRequest(response.Metadata);
    }
}