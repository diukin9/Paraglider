using Paraglider.Domain.NoSQL.ValueObjects;

namespace Paraglider.Domain.NoSQL.Abstractions
{
    public interface IOfferServices
    {
        public List<Service> Services { get; set; }
    }
}
