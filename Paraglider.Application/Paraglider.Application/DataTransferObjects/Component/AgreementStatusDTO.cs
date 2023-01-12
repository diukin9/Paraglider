namespace Paraglider.Application.DataTransferObjects;

using Paraglider.Infrastructure.Common.Interfaces;
using System.Text.Json.Serialization;

public class AgreementStatusDTO : IDataTransferObject
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("value")]
    public int Value { get; set; }
}
