using Paraglider.Domain.Abstractions;
using Paraglider.Infrastructure.Abstractions;

namespace Paraglider.Domain.Entities;

public class BanquetHall : WeddingComponent, IAggregateRoot
{
    public virtual List<Hall> Premises { get; set; } = new List<Hall>();
}
