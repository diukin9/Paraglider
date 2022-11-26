﻿using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Data.EntityFrameworkCore.Factories;

public class UserFactory
{
    public static ApplicationUser Create(UserData data)
    {
        var id = Guid.NewGuid();

        var user = new ApplicationUser()
        {
            Id = id,
            FirstName = data.FirstName,
            Surname = data.Surname,
            UserName = data.Username,
            CityId = data.City.Id,
            City = data.City,
            Email = data.Email,
            EmailConfirmed = data.EmailConfirmed,
            PhoneNumber = data.PhoneNumber,
            Planning = new Planning()
            {
                Id = Guid.NewGuid()
            }
        };

        if (data.Provider.HasValue && !string.IsNullOrEmpty(data.ExternalId))
        {
            user.ExternalAuthInfo = new List<ExternalAuthInfo>()
            {
                new ExternalAuthInfo()
                {
                    Id = Guid.NewGuid(),
                    ExternalProvider = data.Provider.Value,
                    ExternalId = data.ExternalId
                }
            };
        }

        return user;
    } 
}

public class UserData
{
    public readonly string FirstName = null!;
    public readonly string Surname = null!;
    public readonly string Username = null!;
    public readonly City City = null!;
    public readonly string? Email;
    public readonly bool EmailConfirmed;
    public readonly AuthProvider? Provider;
    public readonly string? ExternalId;
    public readonly string? PhoneNumber;

    public UserData(
        string firstName,
        string surname,
        string username,
        City city,
        string? email = null,
        bool emailConfirmed = false,
        AuthProvider? provider = null,
        string? externalId = null,
        string? phoneNumber = null)
    {
        if (city is null) throw new ArgumentException(ExceptionMessages.ObjectIsNull(nameof(City)));

        foreach (var field in new List<string>() { firstName, surname, username, city!.Name })
        {
            if (string.IsNullOrEmpty(field))
            {
                throw new ArgumentException(ExceptionMessages.NullOrEmpty(nameof(field)));
            }
        }

        FirstName = firstName;
        Surname = surname;
        Username = username;
        City = city;
        Email = email;
        EmailConfirmed = emailConfirmed;
        PhoneNumber = phoneNumber;
        Provider = provider;
        ExternalId = externalId;
    }
}