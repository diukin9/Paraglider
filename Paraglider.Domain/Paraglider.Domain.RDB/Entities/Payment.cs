using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Domain.RDB.Entities;

public class Payment : IIdentified
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public PaymentStatus Status { get; set; }
    public decimal? Sum { get; set; }
}
