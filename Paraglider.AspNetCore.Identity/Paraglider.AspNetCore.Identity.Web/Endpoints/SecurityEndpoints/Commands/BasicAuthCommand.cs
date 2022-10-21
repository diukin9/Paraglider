using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.AspNetCore.Identity.Domain.Entities;
using Paraglider.AspNetCore.Identity.Infrastructure.Responses.OperationResult;
using Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.ViewModels;
using static Paraglider.AspNetCore.Identity.Infrastructure.AppData;

namespace Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Commands
{
    public record BasicAuthCommand(BasicAuthViewModel Model) : IRequest<OperationResult>;

    public class BasicAuthHandler : IRequestHandler<BasicAuthCommand, OperationResult>
    {
        private readonly ILogger<BasicAuthHandler> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IValidator<BasicAuthViewModel> _validator;

        public BasicAuthHandler(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IValidator<BasicAuthViewModel> validator,
            ILogger<BasicAuthHandler> logger)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _validator = validator;
        }

        public async Task<OperationResult> Handle(BasicAuthCommand request, CancellationToken cancellationToken)
        {
            var operation = OperationResult.CreateResult();

            var validateResult = await _validator.ValidateAsync(request.Model);
            if (!validateResult.IsValid)
            {
                validateResult.Errors.ForEach(x =>
                {
                    _logger.LogError(Messages.Validation_FailedValidation(request.Model.GetType(), x.ErrorMessage));
                });
                operation.AddError(string.Join("; ", validateResult.Errors));
                return operation;
            }

            var user = await _userManager.FindByEmailAsync(request.Model.Email);
            if (user == null)
            {
                _logger.LogError(Messages.BasicAuth_UserNotFound(request.Model.Email));
                operation.AddError(Messages.BasicAuth_UserNotFound(request.Model.Email));
                return operation;
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, request.Model.Password, true, false);
            if (!signInResult.Succeeded)
            {
                _logger.LogError(Messages.BasicAuth_WrongPassword(request.Model.Email));
                operation.AddError(Messages.BasicAuth_WrongPassword(request.Model.Email));
                return operation;
            }

            _logger.LogInformation(Messages.BasicAuth_SuccessfulAuth(request.Model.Email));
            operation.AddSuccess(Messages.BasicAuth_SuccessfulAuth(request.Model.Email));
            return operation;
        }
    }
}
