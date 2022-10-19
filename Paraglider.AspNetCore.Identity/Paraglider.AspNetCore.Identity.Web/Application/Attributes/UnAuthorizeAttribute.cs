using Microsoft.AspNetCore.Mvc.Filters;
using Paraglider.AspNetCore.Identity.Domain.Exceptions;
using static Paraglider.AspNetCore.Identity.Infrastructure.AppData.AppData;

namespace Paraglider.AspNetCore.Identity.Web.Application.Attributes
{
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
            context!.Result = new StatuCodeResultWithMessage(403, Messages.UserAlreadyAuthorized(email!));
            return Task.CompletedTask;
        }
    }
}
