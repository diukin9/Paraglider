using Paraglider.Domain.NoSQL.ValueObjects;

namespace Paraglider.API.DataTransferObjects.Abstractions
{
    public interface IOfferServices
    {
        public List<Service> Services { get; set; }
    }
}
