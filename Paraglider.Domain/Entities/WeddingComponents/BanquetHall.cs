using Paraglider.Domain.Abstractions;
using Paraglider.Infrastructure.Abstractions;

namespace Paraglider.Domain.Entities;

public class BanquetHall : WeddingComponent, IAggregateRoot
{
    public virtual List<Premise> Premises { get; set; } = new List<Premise>();
}
