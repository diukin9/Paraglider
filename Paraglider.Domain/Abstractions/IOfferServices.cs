using Paraglider.Domain.Entities;

namespace Paraglider.Domain.Abstractions;

public interface IOfferServices
{
    public List<Service> Services { get; set; }
}
