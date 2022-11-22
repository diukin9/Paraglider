using Paraglider.Domain.Common.ValueObjects;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Domain.RDB.Entities;

public class WeddingComponentDesc : IIdentified
{
    public Guid Id { get; set; }
    public Guid ConfigurationItemId { get; set; }
    public SelectedComponentStatus Status { get; set; }
    public TimeInterval TimeInterval { get; set; } = null!;
    public virtual List<Payment> Payments { get; set; } = new List<Payment>();
}
