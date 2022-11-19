using Paraglider.GorkoClient.Models.Abstractions;

namespace Paraglider.GorkoClient.Models;

public partial class Restaurant : IHaveCityId
{
    public long? CityId => City?.Id;
}