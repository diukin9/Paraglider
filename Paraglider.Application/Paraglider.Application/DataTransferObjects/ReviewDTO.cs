using Paraglider.Infrastructure.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Paraglider.Application.DataTransferObjects;

public class ReviewDTO : IDataTransferObject
{
    [JsonPropertyName("author")]
    public string Author { get; set; } = null!;

    [JsonPropertyName("avatar_url")]
    public string AvatarUrl { get; set; } = null!;

    [JsonPropertyName("text")]
    public string? Text { get; set; }

    [JsonPropertyName("evaluation")]
    public double Evaluation { get; set; }
}
