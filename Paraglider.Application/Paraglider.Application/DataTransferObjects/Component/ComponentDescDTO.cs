using Paraglider.Infrastructure.Common.Interfaces;
using System.Text.Json.Serialization;

namespace Paraglider.Application.DataTransferObjects;

public class ComponentDescDTO : IDataTransferObject
{
    [JsonPropertyName("status")]
    public AgreementStatusDTO Status { get; set; } = null!;

    [JsonPropertyName("interval")]
    public TimeIntervalDTO TimeInterval { get; set; } = null!;

    [JsonPropertyName("payments")]
    public virtual List<PaymentDTO> Payments { get; set; } = new List<PaymentDTO>();
}
