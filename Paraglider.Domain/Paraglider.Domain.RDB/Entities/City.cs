using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Domain.RDB.Entities;

public class City : IAggregateRoot
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<ExternalCityKey> Keys { get; set; } = new HashSet<ExternalCityKey>();
}
