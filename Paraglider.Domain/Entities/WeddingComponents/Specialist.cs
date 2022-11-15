using Paraglider.Domain.Abstractions;
using Paraglider.Infrastructure.Abstractions;

namespace Paraglider.Domain.Entities;

public class Specialist : WeddingComponent, IOfferingServices, IAggregateRoot
{
    public virtual List<Service> Services { get; set; } = new List<Service>();
}
