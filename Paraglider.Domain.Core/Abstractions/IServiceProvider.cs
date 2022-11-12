using Paraglider.Domain.Entities;

namespace Paraglider.Domain.Abstractions
{
    public interface IServiceProvider
    {
        public List<Service> Services { get; set; }
    }
}
