using MediatR;
using Paraglider.AspNetCore.Identity.Domain.Entities;
using Paraglider.AspNetCore.Identity.Domain.Services.Interfaces;
using Paraglider.AspNetCore.Identity.Infrastructure.Responses.OperationResult;
using static Paraglider.AspNetCore.Identity.Infrastructure.AppData;

namespace Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Commands
{
    public record ExternalAuthCommand(string RemoteError, string ReturnUrl) : IRequest<OperationResult>;

    public class ExternalAuthHandler : IRequestHandler<ExternalAuthCommand, OperationResult>
    {
        private readonly ILogger<BasicAuthHandler> _logger;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public ExternalAuthHandler(IAuthService authService, IUserService userService, ILogger<BasicAuthHandler> logger)
        {
            _logger = logger;
            _authService = authService;
            _userService = userService;
        }

        public async Task<OperationResult> Handle(ExternalAuthCommand request, CancellationToken cancellationToken)
        {
            var result = new OperationResult();

            if (!string.IsNullOrEmpty(request.RemoteError))
            {
                result.AddError(request.RemoteError);
                _logger.LogError(result.Metadata!.Message);
                return result;
            }

            ApplicationUser user;

            var loginInfoResult = await _authService.GetExternalLoginInfoAsync();
            if (!loginInfoResult.IsOk)
            {
                result.AddError(loginInfoResult.Metadata!.Message);
                _logger.LogError(result.Metadata!.Message);
                return result;
            }
            var info = loginInfoResult.Result;

            var findUserResult = await _userService.FindUserForExternalAuthAsync(info!);
            if (findUserResult.IsOk)
            {
                user = findUserResult.Result!;
            }
            else
            {
                var createUserResult = await _userService.CreateUserUsingExternalProvider(info!);
                if (!createUserResult.IsOk)
                {
                    result.AddError(createUserResult.Metadata!.Message);
                    _logger.LogError(result.Metadata!.Message);
                    return result;
                }
                _logger.LogInformation(createUserResult.Metadata!.Message);
                user = createUserResult.Result!;
            }

            var addLoginResult = await _userService.AddLoginAsync(info!, user!);
            if (!addLoginResult.IsOk)
            {
                result.AddError(addLoginResult.Metadata!.Message);
                _logger.LogError(result.Metadata!.Message);
                return result;
            }

            var signInResult = await _authService.ExternalLoginSignInAsync(info!);
            if (!signInResult.IsOk)
            {
                result.AddError(signInResult.Metadata!.Message);
                _logger.LogError(result.Metadata!.Message);
                return result;
            }

            _logger.LogInformation(Messages.ExternalAuth_SuccessfullAuth(user.Email, info!.LoginProvider));
            return result;
        }
    }
}
