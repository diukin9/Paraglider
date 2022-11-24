using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;
using Reinforced.Typings.Attributes;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Authorization.Commands;

[TsClass] public record BasicAuthRequest(string Login, string Password) : IRequest<OperationResult>;

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
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IValidator<BasicAuthRequest> _validator;

    public BasicAuthCommandHandler(
        SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager,
        IValidator<BasicAuthRequest> validator)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _validator = validator;
    }

    public async Task<OperationResult> Handle(BasicAuthRequest request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //валидируем полученные данные
        var validateResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
        {
            return operation.AddError(string.Join("; ", validateResult.Errors));
        }

        //получаем пользователя
        var user = await _userManager.FindByEmailAsync(request.Login)
                ?? await _userManager.FindByNameAsync(request.Login);

        if (user is null)
        {
            return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(ApplicationUser)));
        }

        //пытаемся авторизовать
        var signInResult = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);
        if (!signInResult.Succeeded)
        {
            return operation.AddError(ExceptionMessages.WrongPasswordEntered);
        }

        return operation.AddSuccess(Messages.SuccessfulAuth);
    }
}