using Paraglider.Infrastructure.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Paraglider.Application.DataTransferObjects;

public class MediaDTO : IDataTransferObject
{
    [JsonPropertyName("type")]
    public MediaTypeDTO Type { get; set; } = null!;

    [JsonPropertyName("url")]
    public string Url { get; set; } = null!;
}
