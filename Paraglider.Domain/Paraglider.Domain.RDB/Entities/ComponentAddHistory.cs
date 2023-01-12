using Paraglider.Infrastructure.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paraglider.Domain.RDB.Entities;

public class ComponentAddHistory : IAggregateRoot
{
    public Guid Id { get; set; }

    public Guid ComponentId { get; set; }

    public virtual Component Component { get; set; } = null!;

    public Guid UserId { get; set; }

    public virtual ApplicationUser User { get; set; } = null!;
}
