using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Data.EntityFrameworkCore.Factories;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.MailService;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Registration.Commands;

public record RegisterUserCommand : IRequest<OperationResult<ApplicationUser>> //TODO возвращать DTO
{
    public string Email { get; init; } = null!;

    public string Password { get; init; } = null!;

    public string FirstName { get; init; } = null!;

    public string Surname { get; init; } = null!;

    public Guid CityId { get; init; }

    public string? PhoneNumber { get; init; }
}

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    private const string PhoneRegex = @"((\+7|7|8)+([0-9]){10})$"; //TODO лучше вынести

    public RegisterUserCommandValidator()
    {
        RuleSet(DefaultRuleSetName, () =>
        {
            RuleFor(e => e.Email).NotEmpty().EmailAddress();
            RuleFor(e => e.Password).NotEmpty().Length(8);
            RuleFor(e => e.FirstName).NotEmpty();
            RuleFor(e => e.Surname).NotEmpty();
            RuleFor(e => e.CityId).NotEmpty();
            RuleFor(e => e.PhoneNumber)
                .SetValidator(new RegularExpressionValidator<RegisterUserCommand>(PhoneRegex));
        });
    }
}

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, OperationResult<ApplicationUser>>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ICityRepository cityRepository;
    private readonly IValidator<RegisterUserCommand> validator;

    public RegisterUserHandler(UserManager<ApplicationUser> userManager,
        IValidator<RegisterUserCommand> validator,
        ICityRepository cityRepository)
    {
        this.userManager = userManager;
        this.cityRepository = cityRepository;
        this.validator = validator;
    }

    public async Task<OperationResult<ApplicationUser>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var operation = new OperationResult<ApplicationUser>();

        var validateResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
            return operation.AddError(string.Join(';', validateResult.Errors));

        if (await userManager.FindByEmailAsync(request.Email) != null)
            return operation.AddError(ExceptionMessages.UserWithEmailAlreadyExist(request.Email));
        
        var city = await cityRepository.FindByIdAsync(request.CityId)
                ?? await cityRepository.GetDefaultCity();

        var user = UserFactory.Create(new UserData(request.FirstName,
            request.Surname,
            request.Email,
            city,
            request.Email,
            phoneNumber: request.PhoneNumber));

        var identityResult = await userManager.CreateAsync(user, request.Password);

        if (!identityResult.Succeeded)
            return operation.AddError(string.Join(';', identityResult.Errors));

        return operation.AddSuccess(Messages.SuccessfulRegistration, user);
    }
}