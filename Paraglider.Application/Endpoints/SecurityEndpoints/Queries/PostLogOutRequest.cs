using MediatR;
using Paraglider.Domain.Services.Interfaces;
using Paraglider.Infrastructure.Extensions;
using Paraglider.Infrastructure.Responses.OperationResult;
using static Paraglider.Infrastructure.AppData;

namespace Paraglider.Web.Endpoints.SecurityEndpoints.Queries
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
