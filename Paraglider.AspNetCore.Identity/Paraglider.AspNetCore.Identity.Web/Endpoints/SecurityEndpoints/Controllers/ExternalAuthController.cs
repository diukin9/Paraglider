using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.AspNetCore.Identity.Domain.Enums;
using Paraglider.AspNetCore.Identity.Infrastructure.Attributes;
using Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Commands;
using static Paraglider.AspNetCore.Identity.Infrastructure.AppData;

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
        [AllowAnonymous]
        [Route("api/external-auth")]
        [FeatureGroupName("Security")]
        public async Task<IActionResult> ExternalAuth([FromQuery] ExternalAuthProvider provider, [FromQuery] string returnUrl = "/")
        {
            if (provider == ExternalAuthProvider.None) return BadRequest(Messages.ExternalAuth_InvalidProvider(Enum.GetName(provider)!)); 
            var result = await _mediator.Send(new ExternalAuthPropertiesCommand(provider, returnUrl), HttpContext.RequestAborted);
            if (!result.IsOk) return BadRequest(result.Metadata!.Message);
            return Challenge(result.Result!, Enum.GetName(provider)!);
        }

        [AllowAnonymous]
        [Route("api/external-auth-handler")]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> ExternalAuthHandler(string remoteError, string returnUrl)
        {
            var result = await _mediator.Send(new ExternalAuthCommand(remoteError, returnUrl), HttpContext.RequestAborted);
            if (!result.IsOk) return BadRequest(result.Metadata!.Message);
            return Redirect(returnUrl);
        }
    }
}
