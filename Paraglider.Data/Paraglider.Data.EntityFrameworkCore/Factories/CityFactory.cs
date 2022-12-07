using Paraglider.Domain.RDB.Entities;
using static Paraglider.Infrastructure.Common.AppData;

namespace Paraglider.Data.EntityFrameworkCore.Factories;

public class CityFactory
{
    public static City Create(CityData data)
    {
        var city = new City()
        {
            Name = data.Name
        };

        return city;
    }
}

public class CityData
{
    public readonly string Name = null!;

    public CityData(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentException(ExceptionMessages.ValueNullOrEmpty(nameof(name)));
        }

        Name = name;
    }
}
