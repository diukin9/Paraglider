using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.Application.Features.Categories.Commands;
using Paraglider.Application.Features.Categories.Queries;
using ActionResult = Paraglider.Infrastructure.Common.Helpers.ActionResult;

namespace Paraglider.Application.Controllers;

[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}")]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("categories")]
    public async Task<IActionResult> Get(
        [FromQuery] GetAllCategoriesRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return ActionResult.Create(response);
    }

    [HttpGet("user/planning/categories")]
    public async Task<IActionResult> GetUserCategories(
        [FromQuery] GetUserCategoriesRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return ActionResult.Create(response);
    }

    [HttpPost("user/planning/categories")]
    public async Task<IActionResult> AddCategory(
        [FromBody] AddCategoryToUserRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return ActionResult.Create(response);
    }

    [HttpDelete("user/planning/categories")]
    public async Task<IActionResult> RemoveCategory(
        [FromBody] RemoveCategoryFromUserRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(request, cancellationToken);
        return ActionResult.Create(response);
    }
}
