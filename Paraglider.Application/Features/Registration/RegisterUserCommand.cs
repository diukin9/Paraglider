using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Data.Factories;
using Paraglider.Data.Repositories;
using Paraglider.Domain.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Abstractions;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Registration;

public record RegisterUserCommand : IRequest<OperationResult>
{
    public string Email { get; init; } = null!;

    public string Password { get; init; } = null!;

    public string FirstName { get; init; } = null!;

    public string Surname { get; init; } = null!;

    public string? CityKey { get; init; }

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
    private readonly CityRepository cityRepository;
    private readonly IValidator<RegisterUserCommand> validator;

    public RegisterUserHandler(UserManager<ApplicationUser> userManager,
        IUnitOfWork unitOfWork,
        IValidator<RegisterUserCommand> validator)
    {
        this.userManager = userManager;
        cityRepository = (CityRepository) unitOfWork.GetRepository<City>();
        this.validator = validator;
    }

    public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var validateResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validateResult.IsValid)
            return OperationResult.Error(string.Join(';', validateResult.Errors));


        var city = request.CityKey != null
            ? await cityRepository.GetByKeyAsync(request.CityKey)
              ?? await cityRepository.GetDefaultCity()
            : await cityRepository.GetDefaultCity();

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