using Newtonsoft.Json;

namespace Paraglider.Clients.Gorko.Models;

public class Meta
{
    public string? Name { get; set; }
    
    [JsonProperty("total_count")]
    public int TotalCount { get; set; }
    public int Count { get; set; }
    
    [JsonProperty("per_page")]
    public int PerPage { get; set; }
    
    public int Page { get; set; }
    public int Offset { get; set; }
    
    [JsonProperty("pages_count")]
    public int PagesCount { get; set; }
}