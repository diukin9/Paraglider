using Newtonsoft.Json;
using Paraglider.Clients.Gorko.Models.Abstractions;

namespace Paraglider.Clients.Gorko.Models;

public class Price : IHaveId
{
    public long? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }

    [JsonProperty("value1")] public decimal? ValueFrom { get; set; }

    [JsonProperty("value2")] public decimal? ValueTo { get; set; }
}