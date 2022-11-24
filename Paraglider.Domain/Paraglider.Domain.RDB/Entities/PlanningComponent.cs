using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Domain.RDB.Entities;

public class PlanningComponent : IIdentified
{
    public Guid Id { get; set; }
    public Guid ComponentId { get; set; }

    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;

    public Guid ComponentDescId { get; set; }
    public virtual ComponentDesc ComponentDesc { get; set; } = null!;

    public Guid PlanningId { get; set; }
    public virtual Planning Planning { get; set; } = null!;
}
