using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Paraglider.Clients.Gorko.Models;

[JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
public class PagedResult<T>
{
    [JsonIgnore] public IReadOnlyCollection<T> Items { get; set; }
    
    public Meta? Meta { get; set; }

    #region setters
    //костыль для легкого маппинга разных имен в одну коллекцию
    [JsonProperty]
    private IReadOnlyCollection<T> Users
    {
        set => Items = value;
    }

    [JsonProperty]
    private IReadOnlyCollection<T> Roles
    {
        set => Items = value;
    }

    [JsonProperty]
    private IReadOnlyCollection<T> Cities
    {
        set => Items = value;
    }
    
    [JsonProperty]
    private IReadOnlyCollection<T> Cars
    {
        set => Items = value;
    }

    [JsonProperty]
    private IReadOnlyCollection<T> Restaurants
    {
        set => Items = value;
    }

    #endregion
}