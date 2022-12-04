using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Data.EntityFrameworkCore.Factories;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.MailService;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Registration.Commands;

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
}

public class RegisterUserHandler : IRequestHandler<RegisterUserCommand, OperationResult>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ICityRepository cityRepository;
    private readonly IMailService mailService;
    private readonly LinkGenerator linkGenerator;
    private readonly IHttpContextAccessor accessor;
    private readonly IValidator<RegisterUserCommand> validator;

    public RegisterUserHandler(UserManager<ApplicationUser> userManager,
        IValidator<RegisterUserCommand> validator,
        ICityRepository cityRepository,
        IMailService mailService,
        LinkGenerator linkGenerator,
        IHttpContextAccessor accessor)
    {
        this.userManager = userManager;
        this.cityRepository = cityRepository;
        this.mailService = mailService;
        this.linkGenerator = linkGenerator;
        this.accessor = accessor;
        this.validator = validator;
    }

    public async Task<OperationResult> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var validateResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validateResult.IsValid)
            return OperationResult.Error(string.Join(';', validateResult.Errors));

        if (await userManager.FindByEmailAsync(request.Email) != null)
            return OperationResult.Error(ExceptionMessages.UserWithEmailAlreadyExist(request.Email));
        
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

        return OperationResult.Success(Messages.SuccessfulRegistration, user);
    }
}