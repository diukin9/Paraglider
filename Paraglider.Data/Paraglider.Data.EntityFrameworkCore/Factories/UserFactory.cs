using Paraglider.Domain.RDB.Entities;
using Paraglider.Domain.RDB.Enums;

namespace Paraglider.Data.EntityFrameworkCore.Factories;

public class UserFactory
{
    public static ApplicationUser Create(UserData data)
    {
        var user = new ApplicationUser()
        {
            Id = data.Id,
            FirstName = data.FirstName,
            Surname = data.Surname,
            UserName = data.Username,
            CityId = data.City.Id,
            City = data.City,
            Email = data.Email,
            EmailConfirmed = data.EmailConfirmed,
            PhoneNumber = data.PhoneNumber,
            Planning = new Planning() { Id = data.PlanningId },
            PlanningId = data.PlanningId,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        if (data.Provider.HasValue && !string.IsNullOrEmpty(data.ExternalId))
        {
            user.ExternalAuthInfo = new List<ExtlAuthInfo>()
            {
                new ExtlAuthInfo()
                {
                    Id = Guid.NewGuid(),
                    Provider = data.Provider.Value,
                    ExternalId = data.ExternalId
                }
            };
        }

        return user;
    } 
}

public class UserData
{
    public readonly Guid Id = Guid.NewGuid();
    public readonly Guid PlanningId = Guid.NewGuid();
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
        if (city is null) throw new ArgumentException("City was null");

        foreach (var field in new List<string>() { firstName, surname, username, city!.Name })
        {
            if (string.IsNullOrEmpty(field))
            {
                throw new ArgumentException($"{nameof(field)} was null");
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
