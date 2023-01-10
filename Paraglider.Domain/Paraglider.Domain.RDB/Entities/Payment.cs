using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Domain.RDB.Entities;

public class Payment : IIdentified
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public PaymentStatus Status { get; set; }
    public decimal? Sum { get; set; }

    public Guid ComponentDescId { get; set; }
    public virtual ComponentDesc ComponentDesc { get; set; } = null!;
}
