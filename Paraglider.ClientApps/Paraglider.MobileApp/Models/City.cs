using System.Text.Json.Serialization;

namespace Paraglider.MobileApp.Models;

public class City
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    public override string ToString()
    {
        return Name;
    }
}
