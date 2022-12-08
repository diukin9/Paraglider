using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.API.Features.Planning.Commands;

namespace Paraglider.API.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/user/planning")]
public class PlanningController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlanningController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("add-component")]
    public async Task<IActionResult> AddComponent([FromBody] AddComponentToPlanningRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("remove-component")]
    public async Task<IActionResult> RemoveComponent([FromBody] RemoveComponentFromPlanningRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpPost("add-category")]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryToUserRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpDelete("remove-category")]
    public async Task<IActionResult> RemoveCategory([FromBody] RemoveCategoryFromUserRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }
}
