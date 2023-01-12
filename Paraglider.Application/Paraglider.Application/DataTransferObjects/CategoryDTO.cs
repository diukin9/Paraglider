using Paraglider.Infrastructure.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Paraglider.Application.DataTransferObjects;

public class CategoryDTO : IDataTransferObject
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
}
