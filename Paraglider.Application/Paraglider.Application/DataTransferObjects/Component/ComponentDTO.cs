using Paraglider.Infrastructure.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Paraglider.Application.DataTransferObjects;

public class ComponentDTO : IDataTransferObject
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("description")]
    public string? Description { get; set; } = null!;

    [JsonPropertyName("avatar_url")]
    public string? AvatarUrl { get; set; }

    [JsonPropertyName("category")]
    public CategoryDTO Category { get; set; } = null!;

    [JsonPropertyName("city")]
    public CityDTO City { get; set; } = null!;

    [JsonPropertyName("contacts")]
    public List<ContactDTO> Contacts { get; set; } = null!;

    [JsonPropertyName("reviews")]
    public List<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();

    [JsonPropertyName("rating")]
    public double Rating { get; set; }

    [JsonPropertyName("album")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public AlbumDTO Album { get; set; } = null!;

    [JsonPropertyName("manufacture_year")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? ManufactureYear { get; set; }

    [JsonPropertyName("min_rent_length")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TimeSpan? MinRentLength { get; set; }

    [JsonPropertyName("capacity")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Capacity { get; set; }

    [JsonPropertyName("services")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ServiceDTO>? Services { get; set; }

    [JsonPropertyName("halls")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<HallDTO>? Halls { get; set; }
}
