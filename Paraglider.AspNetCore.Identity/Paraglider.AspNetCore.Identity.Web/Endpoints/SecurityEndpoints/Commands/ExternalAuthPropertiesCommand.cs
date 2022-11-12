using MediatR;
using Microsoft.AspNetCore.Authentication;
using Paraglider.AspNetCore.Identity.Domain.Enums;
using Paraglider.AspNetCore.Identity.Infrastructure.Extensions;
using Paraglider.AspNetCore.Identity.Domain.Services.Interfaces;
using Paraglider.AspNetCore.Identity.Infrastructure.Responses.OperationResult;
using System.Net;

namespace Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Commands
{
    public record ExternalAuthPropertiesCommand(ExternalAuthProvider Provider, string ReturnUrl) : IRequest<OperationResult<AuthenticationProperties>>;

    public class ExternalAuthPropertiesHandler : IRequestHandler<ExternalAuthPropertiesCommand, OperationResult<AuthenticationProperties>>
    {
        private readonly IAuthService _authService;

        public ExternalAuthPropertiesHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<OperationResult<AuthenticationProperties>> Handle(ExternalAuthPropertiesCommand request, CancellationToken cancellationToken)
        {
            var operation = new OperationResult<AuthenticationProperties>();

            var provider = Enum.GetName(request.Provider);
            var callbackUrl = $"/api/external-auth-handler?returnUrl={WebUtility.UrlEncode(request.ReturnUrl)}";

            operation!.AddResult(_authService.ConfigureExternalAuthenticationProperties(provider!, callbackUrl).Result);
            return await Task.FromResult(operation);
        }
    }
}
