using Paraglider.Infrastructure.Common.Abstractions;

namespace Paraglider.Domain.RDB.Entities;

public class Category : IAggregateRoot
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual ICollection<ExternalCategoryKey> Keys { get; set; } = new HashSet<ExternalCategoryKey>();
}
