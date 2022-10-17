using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.AspNetCore.Identity.Domain;
using Paraglider.AspNetCore.Identity.Domain.Exceptions;
using Paraglider.AspNetCore.Identity.Infrastructure;
using Paraglider.AspNetCore.Identity.Web.Application;
using Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.ViewModels;

namespace Paraglider.AspNetCore.Identity.Web.Endpoints.SecurityEndpoints.Commands
{
    public static class BasicAuthorize
    {
        public record Command(BasicAuthorizeViewModel Model) : IRequest<OperationResult>;

        public class Handler : IRequestHandler<Command, OperationResult>
        {
            private readonly IMapper _mapper;
            private readonly ILogger<Handler> _logger;
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;
            private readonly IValidator<BasicAuthorizeViewModel> _validator;
            private readonly IHttpContextAccessor _httpContextAccessor;

            public Handler(
                UserManager<ApplicationUser> userManager,
                SignInManager<ApplicationUser> signInManager,
                IValidator<BasicAuthorizeViewModel> validator,
                ILogger<Handler> logger,
                IMapper mapper,
                IHttpContextAccessor httpContextAccessor)
            {
                _mapper = mapper;
                _logger = logger;
                _signInManager = signInManager;
                _userManager = userManager;
                _validator = validator;
                _httpContextAccessor = httpContextAccessor;
            }

            public async Task<OperationResult> Handle(Command request, CancellationToken cancellationToken)
            {
                var operation = OperationResult.CreateResult();

                if (_httpContextAccessor.HttpContext!.User.Identity!.IsAuthenticated)
                {
                    var authorizedEmail = _httpContextAccessor.HttpContext!.User.Identity!.Name;
                    if (string.Compare(authorizedEmail, request.Model.Email, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        operation.AddWarning("User is already authorized");
                    }
                    else
                    {
                        var message = $"Already authorized user with email: {authorizedEmail}";
                        _logger.MicroserviceBasicAuthorize(request.Model.Email, new AuthorizeException(message));
                        operation.AddError(message);
                    }
                    return operation;
                }

                var validateResult = await _validator.ValidateAsync(request.Model);
                if (!validateResult.IsValid)
                {
                    operation.AddError(string.Join("; ", validateResult.Errors));
                    return operation;
                }

                var user = await _userManager.FindByEmailAsync(request.Model.Email);
                if (user == null)
                {
                    operation.AddError($"User with email: {request.Model.Email} not found");
                    return operation;
                }

                var signInResult = await _signInManager.PasswordSignInAsync(user, request.Model.Password, true, false);
                if (!signInResult.Succeeded)
                {
                    operation.AddError("Wrong password");
                    return operation;
                }

                operation.AddSuccess("User is successfully authorized");
                return operation;
            }
        }
    }
}
