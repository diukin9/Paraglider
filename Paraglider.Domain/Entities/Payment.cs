using Paraglider.Domain.Abstractions;
using Paraglider.Domain.Enums;

namespace Paraglider.Domain.Entities;

public class Payment : IIdentified
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public PaymentStatus Status { get; set; }
    public decimal? Sum { get; set; }
}
