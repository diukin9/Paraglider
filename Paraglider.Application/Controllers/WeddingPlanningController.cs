using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.API.Features.WeddingPlannings.Commands;
using Paraglider.API.Features.WeddingPlannings.Queries;

namespace Paraglider.API.Controllers
{
    [Authorize]
    [Route("api/plannings")]
    public class WeddingPlanningController : Controller
    {
        private readonly IMediator _mediator;

        public WeddingPlanningController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            var response = await _mediator.Send(new GetPlanningRequest(), HttpContext.RequestAborted);
            return response.IsOk ? Ok(response) : BadRequest(response);
        }

        [HttpPost("add-component")]
        public async Task<IActionResult> AddComponent([FromBody] AddComponentRequest request)
        {
            var response = await _mediator.Send(request, HttpContext.RequestAborted);
            return response.IsOk ? Ok(response) : BadRequest(response);
        }
    }
}
