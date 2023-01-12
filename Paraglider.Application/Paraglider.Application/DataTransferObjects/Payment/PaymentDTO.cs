using Paraglider.Infrastructure.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Paraglider.Application.DataTransferObjects;

public class PaymentDTO : IDataTransferObject
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("date")]
    public DateOnly Date { get; set; }

    [JsonPropertyName("status")]
    public PaymentStatusDTO Status { get; set; } = null!;

    [JsonPropertyName("sum")]
    public decimal? Sum { get; set; }
}
