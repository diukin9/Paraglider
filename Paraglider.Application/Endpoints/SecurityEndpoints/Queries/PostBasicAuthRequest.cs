using FluentValidation;
using MediatR;
using Paraglider.Domain.Services.Interfaces;
using Paraglider.Infrastructure.Extensions;
using Paraglider.Infrastructure.Responses.OperationResult;
using Paraglider.Web.Endpoints.SecurityEndpoints.ViewModels;
using static Paraglider.Infrastructure.AppData;

namespace Paraglider.Web.Endpoints.SecurityEndpoints.Queries
{
    public record PostBasicAuthRequest(BasicAuthViewModel Model) : IRequest<OperationResult>;

    public class PostBasicAuthRequestHandler : IRequestHandler<PostBasicAuthRequest, OperationResult>
    {
        private readonly ILogger<PostBasicAuthRequestHandler> _logger;
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IValidator<BasicAuthViewModel> _validator;

        public PostBasicAuthRequestHandler(
            IUserService userService,
            IAuthService authService,
            IValidator<BasicAuthViewModel> validator,
            ILogger<PostBasicAuthRequestHandler> logger)
        {
            _logger = logger;
            _authService = authService;
            _userService = userService;
            _validator = validator;
        }

        public async Task<OperationResult> Handle(PostBasicAuthRequest request, CancellationToken cancellationToken)
        {
            var operation = new OperationResult();

            var validateResult = await _validator.ValidateAsync(request.Model, cancellationToken);
            if (!validateResult.IsValid) return operation.AddError(string.Join("; ", validateResult.Errors));

            var findUserResult = await _userService.FindByEmailAsync(request.Model.Login);
            if (findUserResult.Result == null) findUserResult = await _userService.FindByUsernameAsync(request.Model.Login);
            if (!findUserResult.IsOk)
            {
                _logger.LogError(findUserResult.Metadata!.Message);
                return findUserResult.RescheduleResult(ref operation);
            }

            var signInResult = await _authService.PasswordSignInAsync(findUserResult.Result!, request.Model.Password, true, false);
            if (!signInResult.IsOk)
            {
                _logger.LogError(signInResult.Metadata!.Message);
                return operation.RescheduleResult(ref operation);
            }

            _logger.LogInformation(Messages.BasicAuth_SuccessfulAuth(request.Model.Login));
            return operation.AddSuccess(Messages.BasicAuth_SuccessfulAuth(request.Model.Login));
        }
    }
}
