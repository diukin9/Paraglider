using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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

            //TODO create custom exception
            var email = context.HttpContext.User.Identity!.Name;
            context!.Result = new BadRequestObjectResult(Messages.UserAlreadyAuthorized(email!));
            return Task.CompletedTask;
        }
    }
}
