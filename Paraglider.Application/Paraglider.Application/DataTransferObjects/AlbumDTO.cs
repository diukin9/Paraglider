using Paraglider.Infrastructure.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Paraglider.Application.DataTransferObjects;

public class AlbumDTO : IDataTransferObject
{
    [JsonPropertyName("media")]
    public List<MediaDTO> Media { get; set; } = new List<MediaDTO>();
}
