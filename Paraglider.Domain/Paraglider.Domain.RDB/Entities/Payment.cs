using Paraglider.Domain.RDB.Abstractions;
using Paraglider.Domain.RDB.Enums;

namespace Paraglider.Domain.RDB.Entities;

public class Payment : IIdentified
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public PaymentStatus Status { get; set; }
    public decimal? Sum { get; set; }
}
