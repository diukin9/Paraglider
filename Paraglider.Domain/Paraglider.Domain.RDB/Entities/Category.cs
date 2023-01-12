using Paraglider.Infrastructure.Common.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace Paraglider.Domain.RDB.Entities;

public class Category : IAggregateRoot
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<ExtCategoryKey> Keys { get; set; } = new HashSet<ExtCategoryKey>();
}
