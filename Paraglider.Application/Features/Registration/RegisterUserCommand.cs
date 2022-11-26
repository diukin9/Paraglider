using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Data.EntityFrameworkCore.Factories;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Registration;

public record RegisterUserCommand : IRequest<OperationResult>
{
    public string Email { get; init; } = null!;

    public string Password { get; init; } = null!;

    public string FirstName { get; init; } = null!;

    public string Surname { get; init; } = null!;

    public Guid CityId { get; init; }

    public string? PhoneNumber { get; init; }

    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        private const string PhoneRegex = @"((\+7|7|8)+([0-9]){10})$";

        public RegisterUserCommandValidator()
        {
            RuleSet(DefaultRuleSetName, () =>
            {
                RuleFor(e => e.Email).EmailAddress();
                RuleFor(e => e.Password).NotNull().NotEmpty().Length(8);
                RuleFor(e => e.FirstName).NotNull().NotEmpty();
                RuleFor(e => e.Surname).NotNull().NotEmpty();
                RuleFor(e => e.PhoneNumber)
                    .SetValidator(new RegularExpressionValidator<RegisterUserCommand>(PhoneRegex));
            });
        }
    }
}

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, OperationResult>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ICityRepository cityRepository;
    private readonly IValidator<RegisterUserCommand> validator;

    public RegisterUserHandler(UserManager<ApplicationUser> userManager,
        IValidator<RegisterUserCommand> validator, ICityRepository cityRepository)
    {
        this.userManager = userManager;
        this.cityRepository = cityRepository;
        this.validator = validator;
    }

    public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var validateResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validateResult.IsValid)
            return OperationResult.Error(string.Join(';', validateResult.Errors));


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
            return OperationResult.Error(string.Join(';', identityResult.Errors));

        return OperationResult.Success(Messages.SuccessfulRegistration);
    }
}