using Paraglider.AspNetCore.Identity.Domain.Entities;

namespace Paraglider.AspNetCore.Identity.Domain.Abstractions
{
    public interface IHaveWeddingServices
    {
        public List<WeddingService> WeddingServices { get; set; }
    }
}
