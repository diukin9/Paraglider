using Paraglider.Domain.Common.ValueObjects;
using Paraglider.Domain.RDB.Enums;
using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Domain.RDB.Entities;

public class ComponentDesc : IIdentified
{
    public Guid Id { get; set; }
    public ComponentStatus Status { get; set; } = ComponentStatus.PreSelection;
    public TimeInterval? TimeInterval { get; set; }

    public Guid PlanningComponentId { get; set; }
    public virtual PlanningComponent PlanningComponent { get; set; } = null!;

    public virtual List<Payment> Payments { get; set; } = new List<Payment>();
}
