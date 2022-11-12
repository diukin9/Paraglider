using Paraglider.Domain.Entities;

namespace Paraglider.Domain.Abstractions
{
    public interface IHaveWeddingServices
    {
        public List<Service> Services { get; set; }
    }
}
