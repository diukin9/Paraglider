using Paraglider.Infrastructure.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Paraglider.Application.DataTransferObjects;

public class ServiceDTO : IDataTransferObject
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("price")]
    public PriceDTO Price { get; set; } = null!;
}
