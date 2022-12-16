using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Domain.RDB.Entities;

public class UserComponent : IIdentified<Guid>
{
    public Guid Id { get; set; }
    public string ComponentId { get; set; } = null!;

    public Guid UserId { get; set; }
    public virtual ApplicationUser User { get; set; } = null!;
}
