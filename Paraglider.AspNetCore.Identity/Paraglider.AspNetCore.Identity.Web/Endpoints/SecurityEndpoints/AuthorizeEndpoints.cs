using MediatR;
using Microsoft.AspNetCore.Mvc;
using Paraglider.AspNetCore.Identity.Domain;
using Paraglider.AspNetCore.Identity.Web.Application;
using Paraglider.AspNetCore.Identity.Web.Definitions.Base;
using Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Commands;
using Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.ViewModels;

namespace Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints
{
    public class AuthorizeEndpoints : AppDefinition
    {
        public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
        {
            app.MapPost("/api/authorize/", BasicAuthorize);
        }

        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [FeatureGroupName("Authorizations")]
        private async Task<OperationResult> BasicAuthorize(
            [FromServices] IMediator mediator, 
            [FromBody] BasicAuthorizeViewModel model, 
            HttpContext context)
        => await mediator.Send(new BasicAuthorize.Command(model), context.RequestAborted);
    }
}
