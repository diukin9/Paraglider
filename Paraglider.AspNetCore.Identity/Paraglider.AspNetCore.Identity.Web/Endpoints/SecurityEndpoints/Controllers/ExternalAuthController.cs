using MediatR;
using Microsoft.AspNetCore.Mvc;
using Paraglider.AspNetCore.Identity.Infrastructure.Attributes;
using Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Commands;
using Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Queries;

namespace Paraglider.AspNetCore.Identity.Web.Controllers
{
    public class ExternalAuthController : Controller
    {
        private readonly IMediator _mediator;

        public ExternalAuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [UnAuthorize]
        [Route("api/external-auth")]
        [FeatureGroupName("Security")]
        public async Task<IActionResult> ExternalAuth([FromQuery] string provider, [FromQuery] string returnUrl = "/")
        {
            var result = await _mediator.Send(new GetExternalAuthPropertiesRequest(provider, returnUrl), HttpContext.RequestAborted);
            if (!result.IsOk) return BadRequest(result.Metadata!.Message);
            return Challenge(result.Result!, provider);
        }

        [UnAuthorize]
        [Route("api/external-auth-handler")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ExternalAuthHandler(string remoteError, string returnUrl = "/")
        {
            var result = await _mediator.Send(new ExternalAuthCommand(remoteError, returnUrl), HttpContext.RequestAborted);
            if (!result.IsOk) return BadRequest(result.Metadata!.Message);
            return Redirect(Path.Combine("..", returnUrl!));
        }
    }
}
