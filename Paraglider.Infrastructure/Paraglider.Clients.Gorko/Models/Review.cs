using Newtonsoft.Json;
using Paraglider.Clients.Gorko.Models.Abstractions;

namespace Paraglider.Clients.Gorko.Models;

public class Review : IHaveId
{
    public long? Id { get; set; }
    public string? UserName { get; set; }
    public string? Text { get; set; }
    [JsonProperty("createdAt")] public long CreatedAtTimestamp { get; set; }
    [JsonProperty("userImg")] public string? UserAvatar { get; set; }
    [JsonProperty("mark")] public List<Mark> Marks { get; set; } = new List<Mark>();


}

public class Mark
{
    public string Name { get; set; } = null!;
    public double Rating { get; set; }
}