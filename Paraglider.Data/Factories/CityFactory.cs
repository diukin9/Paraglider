using Paraglider.Domain.Entities;
using static Paraglider.Infrastructure.AppData;

namespace Paraglider.Data.Factories
{
    public class CityFactory
    {
        public static City Create(CityData data)
        {
            var city = new City()
            {
                Id = Guid.NewGuid(),
                Name = data.Name,
                Key = data.Key
            };

            return city;
        }
    }

    public class CityData
    {
        public readonly string Name = null!;
        public readonly string Key = null!;

        public CityData(string name, string key)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(Exceptions.NullOrEmptyField(nameof(name)));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException(Exceptions.NullOrEmptyField(nameof(key)));
            }

            Name = name;
            Key = key;
        }
    }
}
