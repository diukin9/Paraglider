using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Domain.RDB.Entities;

public class UserComponent : IIdentified
{
    public Guid Id { get; set; }
    public Guid ComponentId { get; set; }

    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; } = null!;
}
