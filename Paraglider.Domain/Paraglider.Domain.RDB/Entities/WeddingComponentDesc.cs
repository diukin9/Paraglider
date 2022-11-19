using Paraglider.Domain.NoSQL.ValueObjects;
using Paraglider.Domain.RDB.Abstractions;
using Paraglider.Domain.RDB.Enums;

namespace Paraglider.Domain.RDB.Entities;

public class WeddingComponentDesc : IIdentified
{
    public Guid Id { get; set; }
    public Guid ConfigurationItemId { get; set; }
    public SelectedComponentStatus Status { get; set; }
    public TimeInterval TimeInterval { get; set; } = null!;
    public virtual List<Payment> Payments { get; set; } = new List<Payment>();
}
