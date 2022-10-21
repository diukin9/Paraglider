using Microsoft.AspNetCore.Mvc.Filters;
using Paraglider.AspNetCore.Identity.Infrastructure.Responses;
using static Paraglider.AspNetCore.Identity.Infrastructure.AppData;

namespace Paraglider.AspNetCore.Identity.Infrastructure.Attributes
{
    /// <summary>
    /// Unauthorize attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UnAuthorizeAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.HttpContext.User.Identity!.IsAuthenticated)
            {
                return base.OnActionExecutionAsync(context, next);
            }

            var email = context.HttpContext.User.Identity!.Name;
            context!.Result = new StatusCodeResultWithMessage(403, Messages.Auth_UserAlreadyAuthorized(email!));
            return Task.CompletedTask;
        }
    }
}
