using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Domain.RDB.Entities;

public class Planning : IAggregateRoot
{
    public Guid Id { get; set; }
    public DateOnly? WeddingDate { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
    public virtual ICollection<PlanningComponent> PlanningComponents { get; set; } = new List<PlanningComponent>();
}
