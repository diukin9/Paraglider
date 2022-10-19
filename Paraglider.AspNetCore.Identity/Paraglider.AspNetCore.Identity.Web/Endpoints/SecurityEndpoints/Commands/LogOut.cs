using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.AspNetCore.Identity.Infrastructure.Data;
using Paraglider.AspNetCore.Identity.Infrastructure.OperationResults;
using static Paraglider.AspNetCore.Identity.Infrastructure.AppData.AppData;

namespace Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Commands
{
    public static class LogOut
    {
        public record Command() : IRequest<OperationResult>;

        public class Handler : IRequestHandler<Command, OperationResult>
        {
            private readonly ILogger<Handler> _logger;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                ILogger<Handler> logger,
                IHttpContextAccessor httpContextAccessor)
            {
                _logger = logger;
                _signInManager = signInManager;
                _userManager = userManager;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<OperationResult> Handle(Command request, CancellationToken cancellationToken)
            {
                var operation = OperationResult.CreateResult();
                var email = _httpContextAccessor.HttpContext!.User.Identity!.Name;
                await _signInManager.SignOutAsync();

                _logger.LogInformation(Messages.SuccessfullyLogOut(email!));
                operation.AddSuccess(Messages.SuccessfullyLogOut(email!));

                return operation;
            }
        }
    }
}
