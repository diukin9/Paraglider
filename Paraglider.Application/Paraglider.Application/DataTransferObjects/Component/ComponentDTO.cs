using Newtonsoft.Json;
using Paraglider.Application.DataTransferObjects;
using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public class ComponentDTO : IDataTransferObject
{
    public string Id { get; set; } = null!;

    public CategoryDTO Category { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? Description { get; set; } = null!;

    public string? AvatarUrl { get; set; }

    public CityDTO City { get; set; } = null!;

    public AlbumDTO Album { get; set; } = null!;

    public ContactsDTO Contacts { get; set; } = null!;

    public List<ReviewDTO> Reviews { get; set; } = new List<ReviewDTO>();

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public DateTime? ManufactureYear { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public TimeSpan? MinRentLength { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public int? Capacity { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public List<ServiceDTO>? Services { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public List<HallDTO>? Halls { get; set; }
}
