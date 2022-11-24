using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Domain.RDB.Entities;

public class Planning : IAggregateRoot
{
    public Guid Id { get; set; }
    public DateOnly? WeddingDate { get; set; }

    public virtual List<Category> Categories { get; set; } = new List<Category>();
    public virtual List<PlanningComponent> PlanningComponents { get; set; } = new List<PlanningComponent>();
}
