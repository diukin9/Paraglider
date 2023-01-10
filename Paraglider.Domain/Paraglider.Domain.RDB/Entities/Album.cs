using Paraglider.Infrastructure.Common.Interfaces;

namespace Paraglider.Domain.RDB.Entities;

public class Album : IIdentified
{
    public Guid Id { get; set; }
    public virtual ICollection<Media> Media { get; set; } = new HashSet<Media>();
}