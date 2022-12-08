using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.API.DataTransferObjects;

public class PaymentDTO : IDataTransferObject
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public PaymentStatusDTO Status { get; set; } = null!;
    public decimal? Sum { get; set; }
}
