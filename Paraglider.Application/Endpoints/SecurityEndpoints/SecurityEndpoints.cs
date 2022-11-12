using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Paraglider.Infrastructure.Attributes;
using Paraglider.Infrastructure.Responses.OperationResult;
using Paraglider.Web.Definitions.Base;
using Paraglider.Web.Endpoints.SecurityEndpoints.Queries;
using Paraglider.Web.Endpoints.SecurityEndpoints.ViewModels;

namespace Paraglider.Web.Endpoints.SecurityEndpoints
{
    public class SecurityEndpoints : AppDefinition
    {
        public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
        {
            app.MapPost("/api/basic-auth/", BasicAuth);
            app.MapPost("/api/log-out", LogOut);
        }

        [AllowAnonymous]
        [FeatureGroupName("Security")]
        private async Task<OperationResult> BasicAuth([FromServices] IMediator mediator, [FromBody] BasicAuthViewModel model, HttpContext context)
            => await mediator.Send(new PostBasicAuthRequest(model), context.RequestAborted);

        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [FeatureGroupName("Security")]
        private async Task<OperationResult> LogOut([FromServices] IMediator mediator, HttpContext context)
            => await mediator.Send(new PostLogOutRequest(), context.RequestAborted);
    }
}
