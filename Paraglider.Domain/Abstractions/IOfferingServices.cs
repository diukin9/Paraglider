using Paraglider.Domain.Entities;

namespace Paraglider.Domain.Abstractions;

public interface IOfferingServices
{
    public List<Service> Services { get; set; }
}
