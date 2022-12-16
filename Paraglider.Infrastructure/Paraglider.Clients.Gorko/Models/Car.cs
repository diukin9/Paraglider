using Newtonsoft.Json;
using Paraglider.Clients.Gorko.Models.Abstractions;

namespace Paraglider.Clients.Gorko.Models;

public class Car : IHaveId
{
    public long? Id { get; set; }
    public string? Name { get; set; }
    public City? City { get; set; }

    public string? Text { get; set; }

    public ICollection<CatalogMedia>? Media { get; set; }

    [JsonProperty("params")] public CarParameters? Parameters { get; set; }

    public ICollection<Contact>? Contacts { get; set; }


    public long? CityId => City?.Id;
    public string? CityName => City?.Name;
}