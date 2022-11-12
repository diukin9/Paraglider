using MediatR;
using Paraglider.AspNetCore.Identity.Domain.Services.Interfaces;
using Paraglider.AspNetCore.Identity.Infrastructure.Extensions;
using Paraglider.AspNetCore.Identity.Infrastructure.Responses.OperationResult;
using static Paraglider.AspNetCore.Identity.Infrastructure.AppData;

namespace Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Queries
{
    public record PostLogOutRequest() : IRequest<OperationResult>;

    public class PostLogOutRequestHandler : IRequestHandler<PostLogOutRequest, OperationResult>
    {
        private readonly ILogger<PostLogOutRequestHandler> _logger;
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostLogOutRequestHandler(
            IAuthService authService,
            ILogger<PostLogOutRequestHandler> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OperationResult> Handle(PostLogOutRequest request, CancellationToken cancellationToken)
        {
            var operation = new OperationResult();
            var username = _httpContextAccessor.HttpContext!.User.Identity!.Name;
            await _authService.SignOutAsync();

            _logger.LogInformation(Messages.LogOut_SuccessfulLogOut(username!));
            return operation.AddSuccess(Messages.LogOut_SuccessfulLogOut(username!));
        }
    }
}
