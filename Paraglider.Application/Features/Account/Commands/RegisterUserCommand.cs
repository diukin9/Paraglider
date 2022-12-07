using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.API.DataTransferObjects;
using Paraglider.Data.EntityFrameworkCore.Factories;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Attributes;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Helpers;
using System.ComponentModel.DataAnnotations;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.API.Features.Account.Commands;

public class RegisterUserRequest : IRequest<OperationResult>
{
    [Required, EmailAddress] public string Email { get; set; }
    [Required, MinLength(8)] public string Password { get; set; }
    [Required] public string FirstName { get; set; }
    [Required] public string Surname { get; set; }
    [Phone] public string? PhoneNumber { get; set; }     //TODO сделать кастомный атрибут PhoneNumber
    [Required, NotEmptyGuid] public Guid CityId { get; set; }

    public RegisterUserRequest(string email, string password, 
        string firstName, string surname, string? phoneNumber, Guid cityId)
    {
        Email = email;
        Password = password;
        FirstName = firstName;
        Surname = surname;
        PhoneNumber = phoneNumber;
        CityId = cityId;
    }
}

public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, OperationResult>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ICityRepository cityRepository;
    private readonly IHttpContextAccessor _accessor;

    public RegisterUserHandler(
        UserManager<ApplicationUser> userManager,
        ICityRepository cityRepository,
        IHttpContextAccessor accessor)
    {
        this.userManager = userManager;
        this.cityRepository = cityRepository;
        _accessor = accessor;
    }

    public async Task<OperationResult> Handle(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new OperationResult();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        if (await userManager.FindByEmailAsync(request.Email) != null)
            return operation.AddError($"Пользователь с email: '{request.Email}' уже существует");

        var city = await cityRepository.FindByIdAsync(request.CityId);
        if (city is null) return operation.AddError(ExceptionMessages.ObjectNotFound(nameof(City)));

        var user = UserFactory.Create(new UserData(
            request.FirstName,
            request.Surname,
            request.Email,
            city,
            request.Email,
            phoneNumber: request.PhoneNumber));

        var identityResult = await userManager.CreateAsync(user, request.Password);

        if (!identityResult.Succeeded)
            return operation.AddError(string.Join(';', identityResult.Errors));

        return operation.AddSuccess("Регистрация прошла успешно.");
    }
}