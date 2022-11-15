using Paraglider.Domain.Abstractions;

namespace Paraglider.Domain.Entities;

public class Album : IIdentified
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public virtual List<Media> Media { get; set; } = new List<Media>();
}
