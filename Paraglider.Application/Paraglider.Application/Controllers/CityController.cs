using MediatR;
using Microsoft.AspNetCore.Mvc;
using Paraglider.Application.Features.City.Queries;
using ActionResult = Paraglider.Infrastructure.Common.Helpers.ActionResult;

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
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetCitiesQuery(), cancellationToken);
        return ActionResult.Create(response);
    }
}