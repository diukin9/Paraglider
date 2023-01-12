using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Paraglider.Data.EntityFrameworkCore.Factories;
using Paraglider.Data.EntityFrameworkCore.Repositories.Interfaces;
using Paraglider.Domain.RDB.Entities;
using Paraglider.Infrastructure.Common;
using Paraglider.Infrastructure.Common.Extensions;
using Paraglider.Infrastructure.Common.Response;

namespace Paraglider.Application.Features.Account.Commands;

public class RegisterUserRequest : IRequest<InternalOperation>
{
    [Required, EmailAddress]
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [Required, MinLength(8)]
    [JsonPropertyName("password")]
    public string Password { get; set; }

    [Required]
    [JsonPropertyName("firstname")]
    public string FirstName { get; set; }

    [Required]
    [JsonPropertyName("surname")]
    public string Surname { get; set; }

    [Phone]
    [JsonPropertyName("phone_number")]
    public string? PhoneNumber { get; set; } //TODO сделать кастомный атрибут PhoneNumber

    [Required]
    [JsonPropertyName("city_id")]
    public Guid CityId { get; set; }

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

public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, InternalOperation>
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ICityRepository cityRepository;

    public RegisterUserHandler(
        UserManager<ApplicationUser> userManager,
        ICityRepository cityRepository)
    {
        this.userManager = userManager;
        this.cityRepository = cityRepository;
    }

    public async Task<InternalOperation> Handle(
        RegisterUserRequest request,
        CancellationToken cancellationToken)
    {
        var operation = new InternalOperation();

        //валидируем полученные данные
        var validation = AttributeValidator.Validate(request);
        if (!validation.IsValid()) return operation.AddError(validation);

        if (await userManager.FindByEmailAsync(request.Email) != null)
        {
            return operation.AddError($"Пользователь с email: '{request.Email}' уже существует");
        }

        var city = await cityRepository.FindByIdAsync(request.CityId);
        if (city is null) return operation.AddError("Город не найден");

        var user = UserFactory.Create(new UserData(
            request.FirstName,
            request.Surname,
            request.Email,
            city,
            request.Email,
            phoneNumber: request.PhoneNumber));

        var identityResult = await userManager.CreateAsync(user, request.Password);

        if (!identityResult.Succeeded)
        {
            return operation.AddError(string.Join(';', identityResult.Errors));
        }

        return operation.AddSuccess();
    }
}