using Paraglider.Infrastructure.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Paraglider.Application.DataTransferObjects;

public class ContactDTO : IDataTransferObject
{
    [JsonPropertyName("key")]
    public string Key { get; set; } = null!;

    [JsonPropertyName("value")]
    public string Value { get; set; } = null!;
}
