using Newtonsoft.Json;
using Paraglider.Clients.Gorko.Models.Abstractions;

namespace Paraglider.Clients.Gorko.Models;

public class Review : IHaveId
{
    public long? Id { get; set; }

    [JsonProperty("created_at")] public long CreatedAtTimestamp { get; set; }

    [JsonProperty("user_name")] public string? UserName { get; set; }

    public string? Text { get; set; }
}