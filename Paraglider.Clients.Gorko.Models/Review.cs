using Newtonsoft.Json;
using Paraglider.GorkoClient.Models.Abstractions;

namespace Paraglider.GorkoClient.Models;

public class Review : IHaveId
{
    public long? Id { get; set; }

    [JsonProperty("created_at")] public long CreatedAtTimestamp { get; set; }

    [JsonProperty("user_name")] public string? UserName { get; set; }

    public string? Text { get; set; }
}