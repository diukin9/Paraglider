using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Data.Repositories;
using Paraglider.Domain.Entities;
using Paraglider.Infrastructure;
using Paraglider.Infrastructure.Exceptions;
using Paraglider.Infrastructure.Extensions;
using static Paraglider.Infrastructure.AppData;

namespace Paraglider.API.Commands;

public class BasicAuthRequest : IRequest<OperationResult>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;

}

public class BasicAuthRequestValidator : AbstractValidator<BasicAuthRequest>
{
    public BasicAuthRequestValidator() => RuleSet(DefaultRuleSetName, () =>
    {
        RuleFor(x => x.Login).MinimumLength(3).MaximumLength(64);
        RuleFor(x => x.Password).MinimumLength(8).MaximumLength(64);
    });
}

public class BasicAuthCommandHandler : IRequestHandler<BasicAuthRequest, OperationResult>
{
    private readonly UserRepository _userRepository;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IValidator<BasicAuthRequest> _validator;

    public BasicAuthCommandHandler(
        SignInManager<ApplicationUser> signInManager,
        IUnitOfWork unitOfWork,
        IValidator<BasicAuthRequest> validator)
    {
        _signInManager = signInManager;
        _userRepository = (UserRepository)unitOfWork.GetRepository<ApplicationUser>();
        _validator = validator;
    }

    public async Task<OperationResult> Handle(BasicAuthRequest request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return operation.AddError(string.Join("; ", validateResult.Errors), new ArgumentException());
        }

        var user = await _userRepository.FindByEmailAsync(request.Login)
                ?? await _userRepository.FindByUsernameAsync(request.Login);

        if (user is null)
        {
            operation.AddError(
                Messages.BasicAuth_UserNotFound(request.Login),
                new NotFoundException(typeof(ApplicationUser))
            );
            return operation;
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);
        if (!signInResult.Succeeded)
        {
            operation.AddError(Messages.BasicAuth_WrongPassword(request.Login), new WrongPasswordException());
            return operation;
        }

        operation.AddSuccess(Messages.BasicAuth_SuccessfulAuth(request.Login));
        return operation;
    }
}