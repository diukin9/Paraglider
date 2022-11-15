using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.API.Commands;

namespace Paraglider.API.Controllers;

[Route("api/[controller]/[action]")]
public class AuthorizationController : Controller
{
    private readonly IMediator _mediator;

    public AuthorizationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> BasicAuthorization([FromBody] BasicAuthRequest request)
    {
        var response = await _mediator.Send(request, HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        var response = await _mediator.Send(new LogoutRequest(), HttpContext.RequestAborted);
        return response.IsOk ? Ok(response) : BadRequest(response);
    }
}
