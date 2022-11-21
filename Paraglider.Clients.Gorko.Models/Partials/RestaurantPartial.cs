using Paraglider.Clients.Gorko.Models.Abstractions;

// ReSharper disable once CheckNamespace
namespace Paraglider.Clients.Gorko.Models;

public partial class Restaurant : IHaveCityId
{
    public long? CityId => City?.Id;
}