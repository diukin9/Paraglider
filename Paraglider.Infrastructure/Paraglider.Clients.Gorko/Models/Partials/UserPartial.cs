using Newtonsoft.Json;
using Paraglider.Clients.Gorko.Models.Abstractions;

// ReSharper disable once CheckNamespace
namespace Paraglider.Clients.Gorko.Models;

//Используется для фильтрации
public partial class User : IHaveCityId
{
    [JsonProperty("city_id")]
    public long? CityId => City?.Id;

    [JsonProperty("city_name")] 
    public string? CityName => City?.Name;
}