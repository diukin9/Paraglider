using Paraglider.Infrastructure.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Paraglider.Application.DataTransferObjects;

public class PaymentStatusDTO : IDataTransferObject
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("value")]
    public int Value { get; set; }
}
