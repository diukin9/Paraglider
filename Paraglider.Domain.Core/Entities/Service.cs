using Paraglider.Domain.Abstractions;
using Paraglider.Domain.ValueObjects;

namespace Paraglider.Domain.Entities
{
    public class Service : IHaveId
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public Price Price { get; set; } = null!;
    }
}
