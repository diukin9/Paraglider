using Paraglider.Domain.RDB.Abstractions;

namespace Paraglider.Domain.RDB.Entities;

public class WeddingPlanning : IIdentified
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CityId { get; set; }
    public virtual City City { get; set; } = null!;
    public List<Guid> ComponentIds { get; set; } = new List<Guid>();
}
