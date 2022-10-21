using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.AspNetCore.Identity.Domain.Entities;
using Paraglider.AspNetCore.Identity.Infrastructure.Responses.OperationResult;
using static Paraglider.AspNetCore.Identity.Infrastructure.AppData;

namespace Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Queries
{
    public record PostLogOutRequest() : IRequest<OperationResult>;

    public class PostLogOutRequestHandler : IRequestHandler<PostLogOutRequest, OperationResult>
    {
        private readonly ILogger<PostLogOutRequestHandler> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostLogOutRequestHandler(
            SignInManager<ApplicationUser> signInManager,
            ILogger<PostLogOutRequestHandler> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OperationResult> Handle(PostLogOutRequest request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult();
            var email = _httpContextAccessor.HttpContext!.User.Identity!.Name;
            await _signInManager.SignOutAsync();

            operation.AddSuccess(Messages.LogOut_SuccessfulLogOut(email!));
            _logger.LogInformation(operation.Metadata!.Message);

            return operation;
        }
    }
}
