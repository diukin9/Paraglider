using Newtonsoft.Json;
using Paraglider.GorkoClient.Models.Abstractions;

namespace Paraglider.GorkoClient.Models;

public class Price : IHaveId
{
    public long? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }

    [JsonProperty("value1")] public decimal? ValueFrom { get; set; }

    [JsonProperty("value2")] public decimal? ValueTo { get; set; }
}