using Newtonsoft.Json;
using Paraglider.Clients.Gorko.Models.Abstractions;

namespace Paraglider.Clients.Gorko.Models;

public partial class Restaurant : IHaveId, IHaveReviews
{
    public long? Id { get; set; }

    public string Role { get; set; } = null!;

    public string? Name { get; set; }
    public City? City { get; set; }

    public IReadOnlyCollection<Contact>? Contacts { get; set; }

    [JsonProperty("coords")] public Coordinates? Coordinates { get; set; }

    public IReadOnlyCollection<Room>? Rooms { get; set; }
    public IReadOnlyCollection<Review>? Reviews { get; set; }
    
    [JsonProperty("text")] public string? Description { get; set; }
    [JsonProperty("cover_url")] public string? Avatar { get; set; }
}

public class Room
{
    public string? Name { get; set; }

    [JsonProperty("params")] public RoomParameters? Parameters { get; set; }

    public IReadOnlyCollection<CatalogMedia>? Media { get; set; }
    
    [JsonProperty("cover_url")] public string? Avatar { get; set; }
}

public class RoomParameters
{
    [JsonProperty("param_banquet_price")] public Parameter<decimal>? PricePerPerson { get; set; }

    [JsonProperty("param_capacity_min")] public Parameter<int>? CapacityMin { get; set; }

    [JsonProperty("param_capacity")] public Parameter<int>? CapacityMax { get; set; }

    [JsonProperty("param_features")] public Parameter<string>? Features { get; set; }
}

public class Coordinates
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}