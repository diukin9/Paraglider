using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Application.DataTransferObjects;

public class PaymentDTO : IDataTransferObject
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public PaymentStatusDTO Status { get; set; } = null!;
    public decimal? Sum { get; set; }
}
