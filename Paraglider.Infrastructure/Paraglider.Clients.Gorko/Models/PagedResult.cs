using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Paraglider.Clients.Gorko.Models;

[JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class PagedResult<T>
{
    [JsonIgnore] public ICollection<T> Items { get; set; } = null!;
    
    public Meta? Meta { get; set; }

    #region setters

    [JsonProperty]
    private ICollection<T> Users
    {
        set => Items = value;
    }

    [JsonProperty]
    private ICollection<T> Roles
    {
        set => Items = value;
    }

    [JsonProperty]
    private ICollection<T> Cities
    {
        set => Items = value;
    }
    
    [JsonProperty]
    private ICollection<T> Cars
    {
        set => Items = value;
    }

    [JsonProperty]
    private ICollection<T> Restaurants
    {
        set => Items = value;
    }

    #endregion
}