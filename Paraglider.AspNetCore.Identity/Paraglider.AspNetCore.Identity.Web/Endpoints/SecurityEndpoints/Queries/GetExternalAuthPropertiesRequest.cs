using MediatR;
using Microsoft.AspNetCore.Authentication;
using Paraglider.AspNetCore.Identity.Domain.Enums;
using Paraglider.AspNetCore.Identity.Domain.Services.Interfaces;
using Paraglider.AspNetCore.Identity.Infrastructure.Responses.OperationResult;
using System.Net;
using static Paraglider.AspNetCore.Identity.Infrastructure.AppData;

namespace Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Queries
{
    public record GetExternalAuthPropertiesRequest(string Provider, string ReturnUrl) : IRequest<OperationResult<AuthenticationProperties>>;

    public class GetExternalAuthPropertiesRequestHandler : IRequestHandler<GetExternalAuthPropertiesRequest, OperationResult<AuthenticationProperties>>
    {
        private readonly IAuthService _authService;
        private readonly ILogger<GetExternalAuthPropertiesRequestHandler> _logger;

        public GetExternalAuthPropertiesRequestHandler(IAuthService authService, ILogger<GetExternalAuthPropertiesRequestHandler> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        public async Task<OperationResult<AuthenticationProperties>> Handle(GetExternalAuthPropertiesRequest request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<AuthenticationProperties>();

            if (!Enum.GetNames<ExternalAuthProvider>().Contains(request.Provider))
            {
                result.AddError(Messages.ExternalAuth_InvalidProvider(request.Provider));
                _logger.LogError(result.Metadata!.Message);
                return result;
            }

            var callbackUrl = $"/api/external-auth-handler?returnUrl={WebUtility.UrlEncode(request.ReturnUrl)}";
            result.Result = _authService.ConfigureExternalAuthenticationProperties(request.Provider, callbackUrl).Result;
            return result;
        }
    }
}
