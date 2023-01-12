using Paraglider.Infrastructure.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paraglider.Domain.RDB.Entities;

public class PlanningComponent : IAggregateRoot
{
    public Guid Id { get; set; }

    public Guid ComponentId { get; set; }

    public virtual Component Component { get; set; } = null!;

    public Guid CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public Guid ComponentDescId { get; set; }

    public virtual ComponentDesc ComponentDesc { get; set; } = null!;

    public Guid PlanningId { get; set; }
}
