using Paraglider.Infrastructure.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Paraglider.Application.DataTransferObjects;

public class HallDTO : IDataTransferObject
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("price")]
    public HallPriceDTO Price { get; set; } = null!;

    [JsonPropertyName("capacity")]
    public CapacityDTO Capacity { get; set; } = null!;

    [JsonPropertyName("album")]
    public AlbumDTO Album { get; set; } = null!;

    [JsonPropertyName("min_price")]
    public decimal? MinimalPrice { get; set; }
}
