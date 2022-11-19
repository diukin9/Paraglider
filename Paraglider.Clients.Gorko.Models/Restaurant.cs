using Newtonsoft.Json;
using Paraglider.GorkoClient.Models.Abstractions;

namespace Paraglider.GorkoClient.Models;

public partial class Restaurant : IHaveId, IHaveReviews
{
    public long? Id { get; set; }

    public string? Name { get; set; }
    public City? City { get; set; }

    public IReadOnlyCollection<Contact>? Contacts { get; set; }

    [JsonProperty("coords")] public Coordinates? Coordinates { get; set; }

    public IReadOnlyCollection<Room>? Rooms { get; set; }
    public IReadOnlyCollection<Review>? Reviews { get; set; }
}

public class Room
{
    public string? Name { get; set; }

    [JsonProperty("params")] public RoomParameters? Parameters { get; set; }

    public IReadOnlyCollection<CatalogMedia>? Media { get; set; }
}

public class RoomParameters
{
    [JsonProperty("param_banquet_price")] public Parameter<decimal>? PricePerPerson { get; set; }

    [JsonProperty("param_capacity_min")] public Parameter<int>? CapacityMin { get; set; }

    [JsonProperty("param_capacity")] public Parameter<decimal>? CapacityMax { get; set; }

    [JsonProperty("param_features")] public Parameter<string>? Features { get; set; }
}

public class Coordinates
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}