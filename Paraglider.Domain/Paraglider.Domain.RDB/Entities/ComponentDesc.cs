using Paraglider.Domain.RDB.Enums;
using Paraglider.Domain.RDB.ValueObjects;
using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Domain.RDB.Entities;

public class ComponentDesc : IIdentified<Guid>
{
    public Guid Id { get; set; }
    public ComponentStatus Status { get; set; } = ComponentStatus.PreSelection;
    public TimeInterval? TimeInterval { get; set; }

    public Guid PlanningComponentId { get; set; }
    public virtual PlanningComponent PlanningComponent { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
