using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.AspNetCore.Identity.Infrastructure.Attributes;
using Paraglider.AspNetCore.Identity.Infrastructure.Responses.OperationResult;
using Paraglider.AspNetCore.Identity.Web.Definitions.Base;
using Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Commands;
using Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Queries;
using Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.ViewModels;

namespace Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints
{
    public class SecurityEndpoints : AppDefinition
    {
        public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
        {
            app.MapPost("/api/basic-auth/", BasicAuth);
            app.MapPost("/api/log-out", LogOut);
        }

        [UnAuthorize]
        [FeatureGroupName("Security")]
        private async Task<OperationResult> BasicAuth([FromServices] IMediator mediator, [FromBody] BasicAuthViewModel model, HttpContext context)
            => await mediator.Send(new BasicAuthCommand(model), context.RequestAborted);

        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [FeatureGroupName("Security")]
        private async Task<OperationResult> LogOut([FromServices] IMediator mediator, HttpContext context)
            => await mediator.Send(new PostLogOutRequest(), context.RequestAborted);
    }
}
