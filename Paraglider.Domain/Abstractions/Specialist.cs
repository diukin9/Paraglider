using Paraglider.Domain.Entities;

namespace Paraglider.Domain.Abstractions
{
    public abstract class Specialist : WeddingComponent, IServiceProvider
    {
        public virtual List<Service> Services { get; set; } = new List<Service>();
    }
}
