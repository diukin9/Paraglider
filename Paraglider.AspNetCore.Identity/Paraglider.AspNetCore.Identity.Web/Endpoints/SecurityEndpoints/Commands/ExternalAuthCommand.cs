using MediatR;
using Paraglider.AspNetCore.Identity.Domain.Entities;
using Paraglider.AspNetCore.Identity.Domain.Services.Interfaces;
using Paraglider.AspNetCore.Identity.Infrastructure.Extensions;
using Paraglider.AspNetCore.Identity.Infrastructure.Responses.OperationResult;
using Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Queries;
using static Paraglider.AspNetCore.Identity.Infrastructure.AppData;

namespace Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Commands
{
    public record ExternalAuthCommand(string RemoteError, string ReturnUrl) : IRequest<OperationResult>;

    public class ExternalAuthHandler : IRequestHandler<ExternalAuthCommand, OperationResult>
    {
        private readonly ILogger<PostBasicAuthRequestHandler> _logger;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public ExternalAuthHandler(IAuthService authService, IUserService userService, ILogger<PostBasicAuthRequestHandler> logger)
        {
            _logger = logger;
            _authService = authService;
            _userService = userService;
        }

        public async Task<OperationResult> Handle(ExternalAuthCommand request, CancellationToken cancellationToken)
        {
            ApplicationUser user;
            var operation = new OperationResult();

            if (!string.IsNullOrEmpty(request.RemoteError))
            {
                _logger.LogError(request.RemoteError);
                return operation.AddError(request.RemoteError);
            }

            var loginInfoResult = await _authService.GetExternalLoginInfoAsync();
            if (!loginInfoResult.IsOk)
            {
                _logger.LogError(operation.Metadata!.Message);
                return loginInfoResult.RescheduleResult(ref operation);
            }
            var info = loginInfoResult.Result;

            var findUserResult = await _userService.FindUserForExternalAuthAsync(info!);
            if (!findUserResult.IsOk)
            {
                var createUserResult = await _userService.CreateUserUsingExternalProvider(info!);
                if (!createUserResult.IsOk)
                {
                    _logger.LogError(createUserResult.Metadata!.Message);
                    return createUserResult.RescheduleResult(ref operation);
                }
                _logger.LogInformation(createUserResult.Metadata!.Message);
                user = createUserResult.Result!;
            }
            else user = findUserResult.Result!;

            var addLoginResult = await _userService.AddLoginAsync(info!, user!);
            if (!addLoginResult.IsOk)
            {
                _logger.LogError(addLoginResult.Metadata!.Message);
                return addLoginResult.RescheduleResult(ref operation);
            }

            var signInResult = await _authService.ExternalLoginSignInAsync(info!);
            if (!signInResult.IsOk)
            {
                _logger.LogError(signInResult.Metadata!.Message);
                return signInResult.RescheduleResult(ref operation);
            }

            _logger.LogInformation(Messages.ExternalAuth_SuccessfullAuth(user.Email ?? user.UserName, info!.LoginProvider));
            return operation;
        }
    }
}
