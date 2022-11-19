using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Data.Repositories;
using Paraglider.Domain.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Abstractions;
using Paraglider.Infrastructure.Common.Extensions;
using Reinforced.Typings.Attributes;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Authorization.Commands;

[TsClass]
public class BasicAuthRequest : IRequest<OperationResult>
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;

}

public class BasicAuthRequestValidator : AbstractValidator<BasicAuthRequest>
{
    public BasicAuthRequestValidator() => RuleSet(DefaultRuleSetName, () =>
    {
        RuleFor(x => x.Login).NotNull().NotEmpty();
        RuleFor(x => x.Password).NotNull().NotEmpty();
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
            return operation.AddError(string.Join("; ", validateResult.Errors));
        }

        var user = await _userRepository.FindByEmailAsync(request.Login)
                ?? await _userRepository.FindByUsernameAsync(request.Login);

        if (user is null)
        {
            return operation.AddError(ExceptionMessages.ObjectIsNull(typeof(ApplicationUser)));
        }

        var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);
        if (!signInResult.Succeeded)
        {
            return operation.AddError(ExceptionMessages.WrongPasswordEntered);
        }

        return operation.AddSuccess(Messages.SuccessfulAuth);
    }
}