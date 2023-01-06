using Paraglider.Infrastructure.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Paraglider.Application.DataTransferObjects;

public class ComponentDTO : IDataTransferObject
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public ComponentStatusDTO Status { get; set; } = null!;

    public CategoryDTO Category { get; set; } = null!;

    public CityDTO City { get; set; } = null!;

    public ContactsDTO Contacts { get; set; } = null!;

    public List<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();

    public int ReviewCount => Reviews?.Count ?? 0;

    public double Rating { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public AlbumDTO Album { get; set; } = null!;

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public DateTime? ManufactureYear { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public TimeSpan? MinRentLength { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public int? Capacity { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<ServiceDTO>? Services { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public List<HallDTO>? Halls { get; set; }
}
